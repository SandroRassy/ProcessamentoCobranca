using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProcessamentoCobranca.API.Models.Enum;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Services;

namespace ProcessamentoCobranca.UnitTests.System.Base
{
    public class TestClientesBase
    {
        public static IConfiguration Configuration { get; }
        public readonly MongoDBSetting mongoDbSettings;
        public readonly ConnectionFactory connectionFactory;
        public readonly ClienteRepository _clienteRepository;
        public readonly ClienteServices _clienteService;

        static TestClientesBase()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();
        }

        public TestClientesBase()
        {
            mongoDbSettings = Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
            connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);
            _clienteRepository = new ClienteRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNClientes.ToString());
            _clienteService = new ClienteServices(_clienteRepository);
        }
    }
}
