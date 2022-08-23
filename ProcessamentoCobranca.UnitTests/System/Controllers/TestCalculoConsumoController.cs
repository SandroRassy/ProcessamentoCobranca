using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestCalculoConsumoController : TestBase
    {
        [Fact]
        public async Task Post_IdBoleto_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new CalculoConsumoController(_cobrancaConsumoService, _cobrancaService, _clienteService);

            /// Act            
            var CalculoConsumoDTO = new CalculoConsumoDTO();
            CalculoConsumoDTO.CPF = "08309184778";
            CalculoConsumoDTO.Key = "9d959854-5c31-43ee-823e-7ff6ece7bd7e";
            var result = (ObjectResult)sut.Post(CalculoConsumoDTO).Result;

            /// Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Post_CPF_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new CalculoConsumoController(_cobrancaConsumoService, _cobrancaService, _clienteService);

            /// Act            
            var CalculoConsumoDTO = new CalculoConsumoDTO();
            CalculoConsumoDTO.CPF = "08309184779";
            CalculoConsumoDTO.Key = "9d959854-5c31-43ee-823e-7ff6ece7bd7e";
            var result = (ObjectResult)sut.Post(CalculoConsumoDTO).Result;

            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
