using MassTransit;
using ProcessamentoCobranca.Services.Interfaces;
using ProcessamentoCobranca.Services.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcessamentoCobranca.WorkerConsumer.Workers
{
    public class WorkerCalculoConsumo : IConsumer<CalculoConsumo>
    {
        //private readonly RequestContext ctx;
        private readonly ICobrancaServices _cobrancaServices;
        private readonly ICobrancaConsumoServices _cobrancaConsumoServices;
        //public WorkerCalculoConsumo(ICobrancaConsumoServices cobrancaConsumoServices, ICobrancaServices cobrancaServices)
        //{
        //    _cobrancaConsumoServices = cobrancaConsumoServices;
        //    _cobrancaServices = cobrancaServices;
        //}
        
        public Task Consume(ConsumeContext<CalculoConsumo> context)
        {
            var cpf = context.Message.cpf;

            var cobranca = _cobrancaServices.Query(new Guid());

            Console.WriteLine($"CPF: [{cpf}");
            return Task.CompletedTask;
        }
    }
}
