using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Books.Domain.Models
{
    public class BookCategory
    {
        public Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
