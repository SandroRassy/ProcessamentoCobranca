
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessamentoCobranca.Services;
using ProcessamentoCobranca.Services.Extensions;
using ProcessamentoCobranca.Services.Interfaces;
using ProcessamentoCobranca.WorkerConsumer.Workers;
using Serilog;

try
{
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

    //Console.WriteLine("Waiting for new messages.");

    //var host = Host.CreateDefaultBuilder(args)
    //    .UseSerilog(Log.Logger)
    //    .ConfigureServices((context, collection) =>
    //    {
    //        //var appSettings = new AppSettings();
    //        //context.Configuration.Bind(appSettings);
    //        collection.AddHttpContextAccessor();

    //        var rabbitMQSettings = builder.Configuration.GetConnectionString("RabbitMq");

    //        collection.AddMassTransit(x =>
    //        {
    //            x.AddDelayedMessageScheduler();
    //            x.AddConsumer<QueueCalculoConsumo>(typeof(QueueCalculoConsumoDefinition));                
    //            //x.AddRequestClient<ConvertVideoEvent>();

    //            x.SetKebabCaseEndpointNameFormatter();

    //            x.UsingRabbitMq((ctx, cfg) =>
    //            {
    //                cfg.Host(rabbitMQSettings);
    //                cfg.UseDelayedMessageScheduler();                    
    //                cfg.ServiceInstance(instance =>
    //                {
    //                    instance.ConfigureJobServiceEndpoints();
    //                    instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
    //                });
    //            });
    //        });
    //    }).Build();

    //await host.StartAsync();
}
catch (Exception)
{

	throw;
}
