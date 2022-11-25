using Library.Books.Business.Events;
using Library.Books.Business.Handlers;
using Library.Books.Database;
using Library.Books.Injection;
using Library.Hub.Rabbit.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Library.Books.Api
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
                    .UseInMemoryDatabase("Library.Authors"));

            services.AddMvc()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAutheticationForAPI(Configuration);

            services.AddInjections();

            services.AddSwagger();

            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

            app.UseCors("MVRCors");

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

            eventBus.Subscribe<BookUpdatedEvent, EventHandler<BookUpdatedEvent>>();
            eventBus.Subscribe<BookCreatedEvent, EventHandler<BookCreatedEvent>>();
            eventBus.Subscribe<BookDeletedEvent, EventHandler<BookDeletedEvent>>();
            eventBus.Subscribe<AuthorUpdatedEvent, EventHandler<AuthorUpdatedEvent>>();
            eventBus.Subscribe<AuthorCreatedEvent, EventHandler<AuthorCreatedEvent>>();
            eventBus.Subscribe<AuthorDeletedEvent, EventHandler<AuthorDeletedEvent>>();
        }
    }
}
