using Library.Books.Domain.Json;
using Library.Books.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        private static void Seed(ModelBuilder modelBuilder)
        {
            using StreamReader r = new("book.json");
            modelBuilder.Entity<BookAuthor>()
               .HasKey(t => new { t.AuthorId, t.BookId });

            modelBuilder.Entity<BookCategory>()
                .HasKey(t => new { t.CategoryId, t.BookId });

            int id = 0;

            string json = r.ReadToEnd();
            List<BookJson> books = JsonConvert.DeserializeObject<List<BookJson>>(json);

            var categories = books
                .SelectMany(x => x.Categories.SelectMany(x => x.Name.Split(',')).Distinct().ToList())
                .Distinct()
                .ToList();

            var catList = new List<Category>();
            var categoryId = 0;
            foreach (var item in categories)
            {
                Category cat = new() { Id = ++categoryId, Name = item };
                catList.Add(cat);
            }

            modelBuilder.Entity<Category>().HasData(catList.ToArray());


            var authors = books
                .SelectMany(x => x.Authors.SelectMany(x => x.Name.Split(',')).Distinct().ToList())
                .Distinct()
                .ToList();

            var authorList = new List<Author>();
            var authorId = 0;
            foreach (var item in authors)
            {
                Author author = new(
                        name: item,
                        surname: "",
                        birth: DateTime.Now.AddYears(-30).AddDays(authorId),
                        id: ++authorId
                    );
                authorList.Add(author);
            }

            modelBuilder.Entity<Author>().HasData(authorList.ToArray());


            List<Book> formattedBook = new();
            foreach (var book in books)
            {
                book.Id = ++id;

                foreach (var author in book.Authors)
                {
                    var authorAddId = authorList.Where(x => x.Name.Equals(author.Name)).First().Id;
                    BookAuthor item = new()
                    {
                        AuthorId = authorAddId,
                        BookId = book.Id
                    };
                    modelBuilder.Entity<BookAuthor>().HasData(item);

                }

                foreach (var category in book.Categories)
                {
                    var catAddId = catList.Where(x => x.Name.Equals(category.Name)).First().Id;
                    BookCategory item = new()
                    {
                        CategoryId = catAddId,
                        BookId = book.Id
                    };
                    modelBuilder.Entity<BookCategory>().HasData(item);

                }

                Book bookItem = new(book.Title, book.Isbn, book.PageCount, book.PublishedDate,
                    book.ThumbnailUrl, book.ShortDescription, book.LongDescription, book.Status, null, null, book.Id);

                modelBuilder.Entity<Book>().HasData(bookItem);
            }
        }
    }
}