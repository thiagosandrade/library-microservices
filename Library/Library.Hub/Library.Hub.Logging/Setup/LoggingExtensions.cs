using Library.Hub.Core.Interfaces;
using Library.Hub.Logging.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Reflection;

namespace Library.Hub.Logging.Setup
{
    public static class LoggingExtensions
    {
        public static void AddCustomLogging(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != "Local")
                services.AddSingleton(typeof(ILogger<>), typeof(CustomLogger<>));

            services.AddSingleton<IMessageEventStore<LogMessageEvent>, LogMessageEventStore>();
        }

        public static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            LoggerConfiguration config = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", environment);

            if (environment == "Development")
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory() + "/Settings")
                    .AddJsonFile(
                        $"appsettingsPackage.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                        optional: false, reloadOnChange: true)
                    .Build();

                config = config
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment));
            }

            Log.Logger = config.CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}