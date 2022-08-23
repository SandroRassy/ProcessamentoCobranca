using DocumentValidator;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Services.Interfaces;
using ProcessamentoCobranca.Services.Models.Shared;
using System.Text.RegularExpressions;

namespace ProcessamentoCobranca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobrancasController : ControllerBase
    {
        private readonly ICobrancaServices _cobrancaServices;
        private readonly IClienteServices _clienteServices;
        private readonly IPublishEndpoint _publishEndpoint;

        public CobrancasController(ICobrancaServices cobrancaServices, IClienteServices clienteServices, IPublishEndpoint publishEndpoint, ICobrancaConsumoServices cobrancaConsumoServices)
        {
            _cobrancaServices = cobrancaServices;
            _clienteServices = clienteServices;
            _publishEndpoint = publishEndpoint;
        }
        // GET: api/<CobrancasController>
        [HttpGet]
        public ActionResult Get(string? cpf, string? mesref)
        {
            try
            {
                if (GetRefMesCpf(cpf, mesref))
                    return Ok(_cobrancaServices.QueryFilter(mesref, cpf));
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        // POST api/<CobrancasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CobrancaDTO cobranca)
        {
            try
            {
                if (CobrancaValidate(cobranca))
                {
                    var obj = CobrancaFill(cobranca);
                    _cobrancaServices.Insert(obj);
                    await _publishEndpoint.Publish<CalculoConsumo>(new
                    {
                        cpf = cobranca.CPF,
                        idBoleto = obj.Key.ToString()
                    });
                }

                return Ok(cobranca);
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        // PUT api/<CobrancasController>/5
        [HttpPut("")]
        public async Task<IActionResult> Put(string dataVencimento, string cpf, string valorcobranca)
        {
            try
            {
                var cobranca = new CobrancaDTO(dataVencimento, cpf, valorcobranca);
                if (CobrancaValidate(cobranca))
                {
                    var obj = CobrancaFill(cobranca);
                    _cobrancaServices.Insert(obj);
                    await _publishEndpoint.Publish<CalculoConsumo>(new
                    {
                        cpf = cobranca.CPF,
                        idBoleto = obj.Key.ToString()
                    });
                }

                return Ok(cobranca);
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        private Cobranca CobrancaFill(CobrancaDTO cobranca)
        {
            DateTime data;
            DateTime.TryParse(DateTime.Now.ToString(), out data);
            return new Cobranca(data, cobranca.CPF, cobranca.ValorCobranca.Replace("R$", String.Empty));
        }

        private bool CobrancaValidate(CobrancaDTO cobranca)
        {
            bool Validado = false;
            if (cobranca != null)
            {
                if (!String.IsNullOrEmpty(cobranca.DataVencimento) || !String.IsNullOrEmpty(cobranca.ValorCobranca) || !String.IsNullOrEmpty(cobranca.CPF))
                {
                    if (cobranca.DataVencimento.Length > 10)
                        throw new Exception($"O campo Data Vencimento não pode ser maior que 10 caracteres");

                    if (cobranca.ValorCobranca.Length < 1)
                        throw new Exception($"O campo Valor Cobrança não pode ser vazio");

                    if (!RealValidate(cobranca.ValorCobranca))
                        throw new Exception($"Valor da cobrança não esta no formatado.");

                    if (!CpfValidation.Validate(cobranca.CPF))
                        throw new Exception($"CPF inválido!");

                    var cliente = _clienteServices.QueryFilter(String.Empty, cobranca.CPF);
                    if (cliente == null)
                        throw new Exception($"CPF não cadastrado!");

                    Validado = true;
                }
                else
                {
                    throw new Exception($"Nenhum dos campos não podem estar em branco.");
                }
            }
            else
            {
                throw new Exception($"Nenhum dos campos não podem estar em branco.");
            }

            return Validado;
        }

        private bool GetRefMesCpf(string? cpf, string? mesref)
        {
            bool Validado = false;

            if (!String.IsNullOrEmpty(cpf))
            {
                if (!CpfValidation.Validate(cpf))
                    throw new Exception($"CPF inválido!");
                Validado = true;
            }
            if (!String.IsNullOrEmpty(mesref))
            {
                if (!MesRefValidate(mesref))
                    throw new Exception($"Mês de referência inválido!");
                Validado = true;
            }

            return Validado;
        }

        private bool RealValidate(string valorcobranca)
        {
            return RegexBase(valorcobranca, @"R\$ ?\d{1,3}(\.\d{3})*,\d{2}");
        }
        private bool MesRefValidate(string mesref)
        {
            return RegexBase(mesref, @"^((0[1-9])|(1[0-2]))/([0-9]{4})$");
        }

        private static bool RegexBase(string valor, string regex)
        {
            bool retorno = false;
            Regex r = new Regex(regex);

            if (r.IsMatch(valor))
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
