using FluentAssertions;
using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Services
{
    public class TestClientesService : TestBase
    {
        [Fact]
        public async Task GetAll_Return()
        {
            /// Act
            var result = _clienteService.QueryAll().ToList();

            /// Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Get_Return()
        {
            /// Act
            var result = _clienteService.QueryFilter(String.Empty, "50974463051");

            /// Assert
            result.Should().NotBeNull();
        }        
    }
}
