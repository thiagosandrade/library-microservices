using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Handlers;
using Library.Hub.Infrastructure.Setup;
using Library.Hub.Logging.Events;
using Library.Hub.Logging.Handlers;
using Library.Hub.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Hub
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INotificationHub, NotificationHub>();
            services.AddScoped<MessageEventHandler>();
            services.AddScoped<LogMessageEventHandler>();

            services.AddSingleton<IMessageEventStore<LogMessageEvent>, LogMessageEventStore>();
            
            services.AddDaprService(Configuration.GetValue<string>("Dapr:httpPort"), Configuration.GetValue<string>("Dapr:grpcPort"));

            services.AddSignalRInjection();

            services.AddSwagger("Library.Hub");

            services.AddControllers().AddDaprForMvc();
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

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/notify");
            });

            app.ApplicationServices.GetService<INotificationHub>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
