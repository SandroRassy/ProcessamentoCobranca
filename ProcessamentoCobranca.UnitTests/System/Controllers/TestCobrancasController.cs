using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestCobrancasController : TestBase
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange            
            var sut = new CobrancasController(_cobrancaService,_clienteService, null, _cobrancaConsumoService);

            /// Act
            var result = (ObjectResult)sut.Get("08309184778");

            /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
