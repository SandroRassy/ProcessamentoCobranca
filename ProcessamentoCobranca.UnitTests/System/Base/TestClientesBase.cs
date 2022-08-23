using Microsoft.Extensions.Configuration;
using ProcessamentoCobranca.API.Models.Enum;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Services;

namespace ProcessamentoCobranca.UnitTests.System.Base
{
    public class TestBase
    {
        public static IConfiguration Configuration { get; }
        public readonly MongoDBSetting mongoDbSettings;
        public readonly ConnectionFactory connectionFactory;
        public readonly ClienteRepository _clienteRepository;
        public readonly CobrancaRepository _cobrancaRepository;
        public readonly CobrancaConsumoRepository _cobrancaConsumoRepository;
        public readonly ClienteServices _clienteService;
        public readonly CobrancaServices _cobrancaService;
        public readonly CobrancaConsumoServices _cobrancaConsumoService;

        static TestBase()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();
        }

        public TestBase()
        {
            mongoDbSettings = Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
            connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

            _clienteRepository = new ClienteRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNClientes.ToString());
            _cobrancaRepository = new CobrancaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNCobrancas.ToString());
            _cobrancaConsumoRepository = new CobrancaConsumoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNCobrancasConsumo.ToString());

            _clienteService = new ClienteServices(_clienteRepository);
            _cobrancaService = new CobrancaServices(_cobrancaRepository);
            _cobrancaConsumoService = new CobrancaConsumoServices(_cobrancaConsumoRepository);
        }
    }
}
