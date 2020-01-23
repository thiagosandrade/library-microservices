// ---------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using Library.Books.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Books.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            Guid first = Guid.NewGuid();
            Guid second = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = first,
                    Name = "History"
                },
                new Category
                {
                    Id = second,
                    Name = "Geography"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book("History of Test", 152, first),
                new Book("Geography of Development", 233, second)
            );
        }
    }
}