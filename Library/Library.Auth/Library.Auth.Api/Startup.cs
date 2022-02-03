using Library.Auth.Business.Events;
using Library.Auth.Business.Handlers;
using Library.Auth.Database;
using Library.Auth.Injection;
using Library.Hub.Rabbit.RabbitMq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Library.Auth.Api
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
                    .UseInMemoryDatabase("Library.Auth"));

            services.AddMvc()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            var secret = Configuration.GetValue<string>("JWT:Secret");
            var key = Encoding.UTF8.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
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

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserLoggedEvent, UserLoggedEventHandler>();

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

        private static void AddInjections(IServiceCollection services)
        {
            services.AddInjections();  
            services.AddControllers().AddNewtonsoftJson();

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Authors", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string []{}
                        }
                    });
                });

            services.AddCors(x => x.AddPolicy("MVRCors", y => 
                y.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));
        }
    }
}
