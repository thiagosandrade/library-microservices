using System;

namespace Library.Books.Domain.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        protected Book()
        {

        }

        public Book(string name, int numberOfPages, Guid categoryId, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            NumberOfPages = numberOfPages;
            CategoryId = categoryId;
        }
    }
}
