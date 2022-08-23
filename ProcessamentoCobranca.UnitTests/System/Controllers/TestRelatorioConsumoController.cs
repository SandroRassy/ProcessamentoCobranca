using FluentAssertions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public  class TestRelatorioConsumoController : TestBase
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            /// Arrange            
            var sut = new RelatorioConsumoController(_cobrancaConsumoService);

            /// Act
            var result = (ObjectResult)sut.Get("RJ", "08/2020");

            /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
