using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using System.IO;

namespace Library.Gateway
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureAppConfiguration((ic, config) =>
            {
                var ocelotConfigPath = Path.Combine(ic.HostingEnvironment.ContentRootPath, "OcelotConfig");
                ocelotConfigPath = Path.Combine(ocelotConfigPath, ic.HostingEnvironment.EnvironmentName);

                config
                    .AddJsonFile($"appsettings.{ic.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddOcelot(ocelotConfigPath, ic.HostingEnvironment)
                    .AddEnvironmentVariables();
            });
    }
}
