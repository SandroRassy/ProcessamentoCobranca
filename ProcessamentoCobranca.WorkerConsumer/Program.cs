
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProcessamentoCobranca.Services.Extensions;
using ProcessamentoCobranca.WorkerConsumer.Workers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog("Worker MassTransit");
Log.Information("Starting Worker");

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

var host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration(builder =>
{
    builder.Sources.Clear();
    builder.AddConfiguration(configuration);
})
.ConfigureServices((context, collection) =>
{
    collection.AddMassTransitExtension(context.Configuration);
})
.Build();


var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    var rabbitMQSettings = builder.Configuration.GetConnectionString("RabbitMq");
    cfg.Host(rabbitMQSettings);
    cfg.ReceiveEndpoint("CalculoConsumo", e =>
    {
        var teste = e.InputAddress;
        e.PrefetchCount = 10;
        e.UseMessageRetry(p => p.Interval(3, 100));
        e.Consumer<WorkerCalculoConsumo>();
    });
});

await busControl.StartAsync(new CancellationToken());

try
{
    Console.WriteLine("Press enter to exit");
    await Task.Run(() => Console.ReadLine());
}
finally
{
    await busControl.StopAsync();
}
