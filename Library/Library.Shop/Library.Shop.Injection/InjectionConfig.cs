using AutoMapper;
using Library.Hub.Rabbit;
using Library.Shop.Business.AutoMapper;
using Library.Shop.Business.CQRS;
using Library.Shop.Database;
using Library.Shop.Database.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shop.Injection
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
