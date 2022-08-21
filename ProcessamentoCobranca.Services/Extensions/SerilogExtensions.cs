using Microsoft.AspNetCore.Builder;
using Serilog.Events;
using Serilog.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Exceptions;
using Microsoft.Extensions.Logging;

namespace ProcessamentoCobranca.Services.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{applicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Log.Logger, true);

            return builder;
        }
    }
}
