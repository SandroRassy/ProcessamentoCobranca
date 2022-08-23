using FluentAssertions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.UnitTests.System.Base;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestCobrancasController : TestBase
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            /// Arrange            
            var sut = new CobrancasController(_cobrancaService,_clienteService, null, _cobrancaConsumoService);

            /// Act
            var result = (ObjectResult)sut.Get("08309184778","08/2020");

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        //[Fact]
        //public async Task Post_ShouldReturn200Status()
        //{
        //    /// Arrange            
        //    não consegui injetar IPublishEndpoint
        //    var sut = new CobrancasController(_cobrancaService, _clienteService, null, _cobrancaConsumoService);
        //    /// Act
        //    var cobrancaDTO = new CobrancaDTO("24/08/2022", "08309184778","R$150,00");
        //    var result = (ObjectResult)sut.Post(cobrancaDTO).Result;

        //    /// Assert
        //    result.StatusCode.Should().Be(200);
        //}
    }
}
