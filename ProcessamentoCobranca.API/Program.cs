using ProcessamentoCobranca.API.Models.Enum;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services;
using ProcessamentoCobranca.Services.Extensions;
using ProcessamentoCobranca.Services.Interfaces;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog("API ProcessamentoCobranca");
    Log.Information("Starting API");

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var mongoDbSettings = builder.Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
    var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

    builder.Services.AddSingleton<IClienteRepository>(p => new ClienteRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNClientes.ToString()));
    builder.Services.AddSingleton<ICobrancaRepository>(p => new CobrancaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNCobrancas.ToString()));
    builder.Services.AddSingleton<ICobrancaConsumoRepository>(p => new CobrancaConsumoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNCobrancasConsumo.ToString()));


    builder.Services.AddTransient<IClienteServices, ClienteServices>();
    builder.Services.AddTransient<ICobrancaServices, CobrancaServices>();
    builder.Services.AddTransient<ICobrancaConsumoServices, CobrancaConsumoServices>();

    builder.Services.AddMassTransitExtension(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}


