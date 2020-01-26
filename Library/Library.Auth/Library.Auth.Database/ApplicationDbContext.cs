using System;
using Library.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Auth.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            Guid first = Guid.NewGuid();
            Guid second = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User("Jack", "Daniels", "test", "12345", "something@gmail.com", first),
                new User("John", "Something", "test2", "12345", "something2@gmail.com", second)
            );
        }
    }
}