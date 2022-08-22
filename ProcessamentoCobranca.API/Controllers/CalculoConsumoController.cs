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

        public CalculoConsumoController(IPublishEndpoint publishEndpoint, ICobrancaConsumoServices cobrancaConsumoServices, ICobrancaServices cobrancaServices)
        {
            _publishEndpoint = publishEndpoint;
            _cobrancaConsumoServices = cobrancaConsumoServices;
            _cobrancaServices = cobrancaServices;
        }



        // GET: api/<CalculoConsumoController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<CalculoConsumoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CalculoConsumoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CalculoConsumoDTO calculoConsumo)
        {
            try
            {
                var boleto = _cobrancaServices.Query(Guid.Parse(calculoConsumo.Key));

                if (boleto != null)
                {
                    var consumo = _cobrancaConsumoServices.Query(Guid.Parse(calculoConsumo.Key));

                    if(consumo != null)
                        throw new Exception($"O boleto já foi calculado o consumo.");

                    _cobrancaConsumoServices.CalcularConsumo(boleto);
                }

                return Ok(calculoConsumo);
            }
            catch (Exception exception)
            {
                Response.StatusCode = 400;
                return new JsonResult($"Erro: {exception.Message}");
            }
        }

        // PUT api/<CalculoConsumoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CalculoConsumoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
