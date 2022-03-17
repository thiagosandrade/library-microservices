using Library.Hub.Rabbit.RabbitMq;
using Library.Shop.Database;
using Library.Shop.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            AddInjections(services);
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
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<BookCreatedEvent, BookCreatedEventHandler>();
            //eventBus.Subscribe<AuthorCreatedEvent, AuthorCreatedEventHandler>();

        }

        private static void AddInjections(IServiceCollection services)
        {
            services.AddInjections();  
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Shop", Version = "v1" });
            });
        }
    }
}
