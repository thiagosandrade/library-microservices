﻿using AutoMapper;
using Library.Auth.Business.AutoMapper;
using Library.Auth.Business.CQRS;
using Library.Auth.Business.Services;
using Library.Auth.Database;
using Library.Auth.Database.Interfaces;
using Library.Hub.Rabbit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

        public static void SeedInMemory(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
