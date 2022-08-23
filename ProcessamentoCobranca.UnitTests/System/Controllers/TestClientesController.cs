using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestClientesController : TestClientesBase
    {      
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange            
            var sut = new ClientesController(_clienteService);
            /// Act
            var result = (OkObjectResult)sut.Get("08309184778");

            /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
