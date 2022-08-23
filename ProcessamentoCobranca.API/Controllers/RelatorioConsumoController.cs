using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.Services.Interfaces;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessamentoCobranca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioConsumoController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICobrancaConsumoServices _cobrancaConsumoServices;


        public RelatorioConsumoController(IPublishEndpoint publishEndpoint, ICobrancaConsumoServices cobrancaConsumoServices)
        {
            _publishEndpoint = publishEndpoint;
            _cobrancaConsumoServices = cobrancaConsumoServices;
        }

        // GET: api/<RelatorioConsumoController>
        [HttpGet]
        public ActionResult Get(string estado, string mesref)
        {
            try
            {
                if (GetRefMesEstado(estado.ToUpper(), mesref))
                    return Ok(_cobrancaConsumoServices.QueryFilter(mesref, estado));
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        private bool GetRefMesEstado(string? estado, string? mesref)
        {
            bool validado = false;
            bool validadoestado = false;
            bool validadomesref = false;

            if (!String.IsNullOrEmpty(estado))
            {
                if (!EstadoValidate(estado))
                    throw new Exception($"Estado inválido!");
                validadoestado = true;
            }
            else
                throw new Exception($"Informe o Estado!");
            if (!String.IsNullOrEmpty(mesref))
            {
                if (!MesRefValidate(mesref))
                    throw new Exception($"Mês de referência inválido!");
                validadomesref = true;
            }
            else
                throw new Exception($"Informe o mês de referência!");

            if (validadoestado && validadomesref)
                return true;
            else
                return false;
        }

        private bool EstadoValidate(string estado)
        {
            return RegexBase(estado, @"^(\s*(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)?)$");
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
