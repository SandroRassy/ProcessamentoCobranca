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
            var result = _clienteService.QueryFilter(String.Empty, "08309184778");

            /// Assert
            result.Should().NotBeNull();
        }

        //[Fact]
        //public async Task Insert_Return()
        //{
        //    /// Act
        //    Cliente cliente = new Cliente("Sandro", "teste_erro", "08309184778");
        //    var result = _clienteService.Insert(cliente);

        //    /// Assert
        //    result.Should().NotBeNull();
        //}
    }
}
