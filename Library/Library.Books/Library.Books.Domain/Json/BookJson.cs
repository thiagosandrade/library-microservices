using Library.Books.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.Books.Domain.Json
{
    public class BookJson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }

        [JsonConverter(typeof(CustomJsonConverter<Author>))]
        public virtual IEnumerable<Author> Authors { get; set; }

        [JsonConverter(typeof(CustomJsonConverter<Category>))]
        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
