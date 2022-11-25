using Library.Auth.Business.AutoMapper;
using Library.Auth.Business.CQRS;
using Library.Auth.Business.Services;
using Library.Auth.Database;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using Library.Hub.Rabbit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Library.Auth.Injection
{
    public static class InjectionConfig
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IAuthService), typeof(AuthService));

            services.AddRabbit();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddMediatR(typeof(BaseHandler).Assembly);
        }

        public static void AddAutheticationForAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetValue<string>("JWT:Secret");
            var key = Encoding.UTF8.GetBytes(secret);

            services
                .AddAuthentication(x =>
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
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Shop", Version = "v1" });
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
        }

        public static void SeedInMemory(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.AddRange(
                new Role("Reader"),
                new Role("SuperUser")
                );

            context.AddRange(
                    new User(
                        "Jack",
                        "Daniels",
                        "test",
                        "12345",
                        "jack.daniels@gmail.com"),
                    new User(
                        "John",
                        "Something",
                        "test2",
                        "12345",
                        "john.something@gmail.com")
                    );

            context.AddRange(
                    new UserRole() { RoleId = 1, UserId = 1 },
                    new UserRole() { RoleId = 2, UserId = 2 }
                );


            context.SaveChanges();
        }
    }
}
