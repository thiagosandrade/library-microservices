using AutoMapper;
using Library.Books.Business.Automapper;
using Library.Books.Business.CQRS;
using Library.Books.Database;
using Library.Books.Database.Interfaces;
using Library.Hub.Rabbit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Books.Injection
{
    public static class InjectionConfig
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddRabbit();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddMediatR(typeof(BaseHandler<>).Assembly);
        }

        public static void SeedInMemory(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
