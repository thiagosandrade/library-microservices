using Library.Books.Domain.Json;
using System.Collections.Generic;

namespace Library.Books.Domain.Models
{
    public class Author : JsonDefault
    {
        public virtual List<BookAuthor> Books { get; set; }
    }
}