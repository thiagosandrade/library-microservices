using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Events;
using Library.Hub.Infrastructure.Handlers;
using Library.Hub.Infrastructure.Setup;
using Library.Hub.Logging.Events;
using Library.Hub.Logging.Handlers;
using Library.Hub.Logging.Setup;
using Library.Hub.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Hub
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INotificationHub, NotificationHub>();
            services.AddScoped<MessageEventHandler>();
            services.AddScoped<LogMessageEventHandler>();

            services.AddSingleton<IMessageEventStore<LogMessageEvent>, LogMessageEventStore>();

            services.AddDaprService();

            services.AddSignalRInjection();

            services.AddMvc(options => options.EnableEndpointRouting = false).AddDaprForMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.WebSocketsConfig();

            app.UseRouting();

            app.UseDaprServices();

            app.UseEndpoints(routes =>
            {
                routes.MapHub<SignalRHub>("/notify");
            });

            app.ApplicationServices.GetService<INotificationHub>();

            app.UseMvc();
        }
    }
}
