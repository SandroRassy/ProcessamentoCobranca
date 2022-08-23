using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestClientesController : TestBase
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange            
            var sut = new ClientesController(_clienteService);

            /// Act
            var result = (ObjectResult)sut.Get("50974463051");

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Post_ErrorEstado_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new ClientesController(_clienteService);

            /// Act            
            var clienteDTO = new ClienteDTO("Sandro", "teste_erro", "50974463051");
            var result = (ObjectResult)sut.Post(clienteDTO);

            /// Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Put_ErrorEstado_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new ClientesController(_clienteService);

            /// Act                        
            var result = (ObjectResult)sut.Put("Sandro", "teste_erro", "50974463051");

            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
