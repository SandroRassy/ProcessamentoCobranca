using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
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

        [Fact]
        public async Task Post_ErrorEstado_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new ClientesController(_clienteService);
            /// Act
            var clienteDTO = new ClienteDTO("Sandro", "teste_erro", "08309184778");
            //var result = (OkObjectResult)sut.Post(clienteDTO);
            var result = (OkObjectResult)sut.Put("Sandro", "teste_erro", "08309184778");

            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
