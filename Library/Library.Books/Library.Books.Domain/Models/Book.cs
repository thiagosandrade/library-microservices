using System;
using System.Collections.Generic;

namespace Library.Books.Domain.Models
{

    public class Book : Entity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }

        public virtual IEnumerable<BookAuthor> Authors { get; set; }

        public virtual IEnumerable<BookCategory> Categories { get; set; }
        
        protected Book()
        {

        }

        public Book(Guid? id, string title, string isbn, int pageCount, DateTime publishedDate, string thumbnailUrl, string shortDescription, string longDescription, 
            string status, IEnumerable<BookAuthor> authors, IEnumerable<BookCategory> categories)
        {
            Id = id ?? Guid.NewGuid();
            Title = title;
            Isbn = isbn;
            PageCount = pageCount;
            PublishedDate = publishedDate;
            ThumbnailUrl = thumbnailUrl;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            Status = status;
            Authors = authors;
            Categories = categories;
        }
    }
}
