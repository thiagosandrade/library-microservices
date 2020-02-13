using System;
using Library.Authors.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Authors.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            Guid first = Guid.NewGuid();
            var firstPlace = new PlaceOfBirth()
            {
                Id = first,
                City = "São Paulo",
                State = "São Paulo",
                Country = "Brazil",
            };

            Guid second = Guid.NewGuid();
            var secondPlace = new PlaceOfBirth()
            {
                Id = second,
                City = "Brasilia",
                State = "Federal District",
                Country = "Brazil",
            };

            modelBuilder.Entity<PlaceOfBirth>().HasData(
                firstPlace,
                secondPlace
            );

            modelBuilder.Entity<Author>().HasData(
                new Author("Jack", "Daniels", new DateTime(1953, 10, 20), first),
                new Author("Jack", "Daniels", new DateTime(1962, 4, 12), second)
            );
        }
    }
}