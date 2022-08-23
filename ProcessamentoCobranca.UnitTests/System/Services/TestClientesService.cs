using FluentAssertions;
using MongoDB.Driver;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Services
{
    public class TestClientesService : TestClientesBase
    {        
        [Fact]
        public async Task GetAllAsync_ReturnTodoCollection()
        {            
            /// Act
            var result = _clienteService.QueryAll().ToList();

            /// Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_ReturnTodoCollection()
        {
            /// Act
            var result = _clienteService.QueryFilter(String.Empty, "08309184778");

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
