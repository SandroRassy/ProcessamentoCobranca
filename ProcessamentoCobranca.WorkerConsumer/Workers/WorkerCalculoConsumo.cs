using MassTransit;
using ProcessamentoCobranca.Services.Interfaces;
using ProcessamentoCobranca.Services.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcessamentoCobranca.WorkerConsumer.Workers
{
    public class WorkerCalculoConsumo : IConsumer<CalculoConsumo>
    {        
        
        public async Task Consume(ConsumeContext<CalculoConsumo> context)
        {            
            var data = context.Message;      
            

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://localhost:5147/api/Clientes/cpf?cpf={data.cpf}");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync("");

                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine($"CPF: [{responseMessage.Content.ReadAsStringAsync().Result}");
                }                
            }

            Console.WriteLine($"CPF: [{data.cpf}");            
        }
    }
}
