using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Books.Domain.Models
{
    public class BookAuthor
    {
        public Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public Guid AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
