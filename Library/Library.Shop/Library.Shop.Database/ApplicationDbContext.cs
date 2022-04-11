using System;
using Library.Shop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Shop.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> Products { get; set; }
    }
}