using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace GenericTools.Logger
{
    public static class SerilogExtention
    {
        public static IHostBuilder UseCustomSerilog(this IHostBuilder host)
        {
            host.UseSerilog((context, config) =>
            {
                config.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"] ?? ""))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"tasklist-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                    .Enrich.WithCorrelationIdHeader("CorrelationId")
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .ReadFrom.Configuration(context.Configuration);
            });

            return host;
        }
    }
}