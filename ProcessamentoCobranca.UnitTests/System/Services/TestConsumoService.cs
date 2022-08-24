using FluentAssertions;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Services
{
    public class TestConsumoService : TestBase
    {
        [Fact]
        public async Task Get_Return()
        {
            /// Act
            var result = _cobrancaConsumoService.QueryFilter("08/2020", "RJ");

            /// Assert
            result.Should().NotBeNull();
        }

    }
}
