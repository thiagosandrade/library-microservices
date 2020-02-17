using Library.Hub.Rabbit;
using Library.Hub.Rabbit.Events;
using Library.Hub.Rabbit.RabbitMq;
using Library.Hub.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Library.Hub
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INotificationHub, NotificationHub>();

            services.AddRabbit();
            services.AddSignalRInjection();

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AddRabbitSubscribers(app);

            app.WebSocketsConfig();

            app.ApplicationServices.GetService<INotificationHub>();

            app.UseMvc();
        }

        private static void AddRabbitSubscribers(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<BookCreatedEvent, BookCreatedEventHandler>();
            //eventBus.Subscribe<AuthorCreatedEvent, AuthorCreatedEventHandler>();

            eventBus.Subscribe<MessageEvent, MessageEventHandler>();
        }
    }
}
