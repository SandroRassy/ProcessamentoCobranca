using MassTransit;
using Microsoft.Extensions.Configuration;
//using MongoDB.Bson.IO;
using Newtonsoft.Json;
using ProcessamentoCobranca.Services.Models.Shared;
using ProcessamentoCobranca.WorkerConsumer.Models.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace ProcessamentoCobranca.WorkerConsumer.Workers
{
    public class WorkerCalculoConsumo : IConsumer<CalculoConsumo>
    {

        public async Task Consume(ConsumeContext<CalculoConsumo> context)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            UrlAPISetting settings = config.GetRequiredSection("UrlAPISetting").Get<UrlAPISetting>();

            var data = context.Message;


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parametros = new CalculoConsumoDTO
                {
                    key = data.idBoleto,
                    cpf = data.cpf
                };

                var content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

                var responseMessage = httpClient.PostAsync(new Uri($"{settings.Url}/api/CalculoConsumo/"), content).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Boleto envio para calculo.");
                }
            }
        }
    }
}
