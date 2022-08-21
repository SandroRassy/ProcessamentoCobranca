
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessamentoCobranca.Services.Extensions;
using ProcessamentoCobranca.WorkerConsumer.Workers;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog("Worker MassTransit");
    Log.Information("Starting Worker");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog(Log.Logger)
        .ConfigureServices((context, collection) =>
        {
            //var appSettings = new AppSettings();
            //context.Configuration.Bind(appSettings);
            collection.AddHttpContextAccessor();

            var rabbitMQSettings = builder.Configuration.GetConnectionString("RabbitMq");

            collection.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.AddConsumer<QueueCalculoConsumo>(typeof(QueueCalculoConsumoDefinition));                
                //x.AddRequestClient<ConvertVideoEvent>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMQSettings);
                    cfg.UseDelayedMessageScheduler();                    
                    cfg.ServiceInstance(instance =>
                    {
                        instance.ConfigureJobServiceEndpoints();
                        instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                    });
                });
            });
        }).Build();

    await host.StartAsync();
}
catch (Exception)
{

	throw;
}
