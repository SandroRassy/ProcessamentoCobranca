using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProcessamentoCobranca.API.Controllers;
using ProcessamentoCobranca.API.Models.Enum;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Services;
using FluentAssertions;

namespace ProcessamentoCobranca.UnitTests.System.Controllers
{
    public class TestClientesController
    {
        private static IConfiguration Configuration { get; }
        private readonly MongoDBSetting mongoDbSettings;
        private readonly ConnectionFactory connectionFactory;
        private readonly ClienteRepository _clienteRepository;
        private readonly ClienteServices _clienteService;

        static TestClientesController()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();
        }

        public TestClientesController()
        {
            mongoDbSettings = Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
            connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);
            _clienteRepository = new ClienteRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNClientes.ToString());
            _clienteService = new ClienteServices(_clienteRepository);
        }

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
