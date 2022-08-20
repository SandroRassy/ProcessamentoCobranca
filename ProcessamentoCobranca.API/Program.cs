using Microsoft.Extensions.DependencyInjection;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Domain.Settings;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Base;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services;
using ProcessamentoCobranca.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);
var ListaCollectionName = mongoDbSettings.CollectionName.ToList();

builder.Services.AddSingleton<IClienteRepository<Cliente>>(p => new ClienteRepository(connectionFactory,mongoDbSettings.DatabaseName, ListaCollectionName[0]));
builder.Services.AddSingleton<ICobrancaRepository<Cobranca>>(p => new CobrancaRepository(connectionFactory,mongoDbSettings.DatabaseName, ListaCollectionName[1]));


builder.Services.AddTransient<IClienteServices, ClienteServices>();
builder.Services.AddTransient<ICobrancaServices, CobrancaServices>();

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

//to do
//ClientesController
//-CPF campo inteiro ou string?
//