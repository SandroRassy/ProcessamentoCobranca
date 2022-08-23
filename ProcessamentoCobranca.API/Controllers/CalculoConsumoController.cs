using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessamentoCobranca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoConsumoController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICobrancaConsumoServices _cobrancaConsumoServices;
        private readonly ICobrancaServices _cobrancaServices;
        private readonly IClienteServices _clienteServices;

        public CalculoConsumoController(IPublishEndpoint publishEndpoint, ICobrancaConsumoServices cobrancaConsumoServices, ICobrancaServices cobrancaServices, IClienteServices clienteServices)
        {
            _publishEndpoint = publishEndpoint;
            _cobrancaConsumoServices = cobrancaConsumoServices;
            _cobrancaServices = cobrancaServices;
            _clienteServices = clienteServices;
        }

        // POST api/<CalculoConsumoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CalculoConsumoDTO calculoConsumo)
        {
            try
            {
                var cliente = _clienteServices.QueryFilter("", calculoConsumo.CPF);

                if (cliente != null)
                {
                    var boleto = _cobrancaServices.Query(Guid.Parse(calculoConsumo.Key));

                    if (boleto != null)
                    {
                        var consumo = _cobrancaConsumoServices.Query(Guid.Parse(calculoConsumo.Key));

                        if (consumo != null)
                            throw new Exception($"O boleto já foi calculado o consumo.");

                        _cobrancaConsumoServices.CalcularConsumo(boleto, cliente);
                        return Ok(calculoConsumo);
                    }
                    else
                    {
                        throw new Exception($"Boleto não existe.");
                    }
                }
                else
                {
                    throw new Exception($"Cliente não existe.");
                }
            }
            catch (Exception exception)
            {
                Response.StatusCode = 400;
                return new JsonResult($"Erro: {exception.Message}");
            }
        }
    }
}
