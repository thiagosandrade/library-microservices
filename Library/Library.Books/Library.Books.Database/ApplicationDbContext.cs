using Library.Books.Domain.Json;
using Library.Books.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            using (StreamReader r = new StreamReader("book.json"))
            {
                modelBuilder.Entity<BookAuthor>()
                   .HasKey(t => new { t.AuthorId, t.BookId });

                modelBuilder.Entity<BookCategory>()
                    .HasKey(t => new { t.CategoryId, t.BookId });

                string json = r.ReadToEnd();
                List<BookJson> books = JsonConvert.DeserializeObject<List<BookJson>>(json);
                List<Book> formattedBook = new List<Book>();
                foreach (var book in books)
                {
                    book.Id = Guid.NewGuid();

                    foreach (var author in book.Authors)
                    {
                        BookAuthor item = new BookAuthor()
                        {
                            AuthorId = author.Id,
                            BookId = book.Id
                        };
                        modelBuilder.Entity<BookAuthor>().HasData(item);

                    }

                    foreach (var category in book.Categories)
                    {
                        BookCategory item = new BookCategory()
                        {
                            CategoryId = category.Id,
                            BookId = book.Id
                        };
                        modelBuilder.Entity<BookCategory>().HasData(item);

                    }

                    Book bookItem = new Book(book.Id, book.Title, book.Isbn, book.PageCount, book.PublishedDate,
                        book.ThumbnailUrl, book.ShortDescription, book.LongDescription, book.Status, null, null);

                    modelBuilder.Entity<Book>().HasData(bookItem);
                }

               
            }
        }
    }
}