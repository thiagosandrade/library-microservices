﻿using Library.Books.Domain.Json;
using System.Collections.Generic;

namespace Library.Books.Domain.Models
{
    public class Category : JsonDefault
    {
        public virtual IEnumerable<BookCategory> Books { get; set; }
    }
}