using FluentAssertions;
using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Services
{
    public class TestCobrancasService : TestBase
    {
        [Fact]
        public async Task Get_Return()
        {
            /// Act
            var result = _cobrancaService.QueryFilter("08/2020", "50974463051");

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
