using MassTransit;
using MassTransit.Metadata;
using Microsoft.Extensions.Logging;
using ProcessamentoCobranca.Services.Models.Shared;
using ProcessamentoCobranca.WorkerConsumer.Workers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.WorkerConsumer.Workers
{
    public class QueueCalculoConsumo : IConsumer<CalculoConsumo>
    {
        private readonly ILogger<QueueCalculoConsumo> _logger;
        public QueueCalculoConsumo(ILogger<QueueCalculoConsumo> logger)
        {
            _logger = logger;
        }
    
        public async Task Consume(ConsumeContext<CalculoConsumo> context)
        {
            var timer = Stopwatch.StartNew();

            try
            {                
                var cpf = context.Message.cpf;

                _logger.LogInformation($"Receive cpf: {cpf}");
                await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<CalculoConsumo>.ShortName);
            }
            catch (Exception ex)
            {
                await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<CalculoConsumo>.ShortName, ex);
            }
        }
    }
}

public class QueueCalculoConsumoDefinition : ConsumerDefinition<QueueCalculoConsumo>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueCalculoConsumo> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
