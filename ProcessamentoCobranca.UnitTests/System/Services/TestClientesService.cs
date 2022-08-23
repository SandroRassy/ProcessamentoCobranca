using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProcessamentoCobranca.API.Models.Enum;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Services;

namespace ProcessamentoCobranca.UnitTests.System.Services
{
    public class TestClientesService
    {
        private static IConfiguration Configuration { get; }
        private readonly MongoDBSetting mongoDbSettings; 
        private readonly ConnectionFactory connectionFactory;
        private readonly ClienteRepository _clienteRepository;
        private readonly ClienteServices _clienteService;

        static TestClientesService()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();
        }

        public TestClientesService()
        {
            mongoDbSettings = Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
            connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);
            _clienteRepository = new ClienteRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNClientes.ToString());
            _clienteService = new ClienteServices(_clienteRepository);
        }

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
