using Library.Hub.Events;
using Library.Hub.Rabbit.RabbitMq;
using Library.Shop.Business.Events;
using Library.Shop.Business.Handlers;
using Library.Shop.Database;
using Library.Shop.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Library.Shop.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                 opt.UseLazyLoadingProxies()
                    .UseInMemoryDatabase("Library.Shop"));

            services.AddMvc()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddAutheticationForAPI(Configuration);

            services.AddInjections();

            services.AddSwagger();
            
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InjectionConfig.SeedInMemory(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AddRabbitSubscribers(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }

        private static void AddRabbitSubscribers(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var eventBus = scope.ServiceProvider.GetService<IEventBus>();

            eventBus.Subscribe<CartProductAddedEvent, EventHandler<CartProductAddedEvent>>();
            eventBus.Subscribe<CartProductRemovedEvent, EventHandler<CartProductRemovedEvent>>();
            eventBus.Subscribe<BookDeletedEvent, BookDeletedEventHandler>();
        }
    }
}
