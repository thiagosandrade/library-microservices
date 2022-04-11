using Library.Books.Domain.Json;
using System;
using System.Collections.Generic;

namespace Library.Books.Domain.Models
{
    public class Author : JsonDefault
    {
        public Author()
        {

        }

        public Author(string name, string surname, DateTime birth, int id = 0)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
        }

        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int Age => DateTime.Now.Year - Birth.Year;

        public virtual List<BookAuthor> Books { get; set; }
    }
}