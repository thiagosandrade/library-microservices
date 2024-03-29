﻿using Library.Auth.Business.AutoMapper;
using Library.Auth.Business.CQRS;
using Library.Auth.Business.Services;
using Library.Auth.Database;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Auth.Injection
{
    public static class InjectionConfig
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IAuthService,AuthService>();

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddMediatR(typeof(BaseHandler).Assembly);
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
