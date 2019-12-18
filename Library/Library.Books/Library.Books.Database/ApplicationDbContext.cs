// ---------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using Library.Books.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "History"
                },
                new Category
                {
                    Id = 2,
                    Name = "Geography"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Name = "History of Test",
                    NumberOfPages = 152,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Name = "Geography of Development",
                    NumberOfPages = 233,
                    CategoryId = 2
                }
            );
        }
    }
}