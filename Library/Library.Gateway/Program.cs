using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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
                config
                    .AddJsonFile($"appsettings.{ic.HostingEnvironment.EnvironmentName}.json", false, true)
                    .AddOcelot(ic.HostingEnvironment)
                    .AddJsonFile("ocelot.json", true)
                    .AddEnvironmentVariables();
            });
    }
}
