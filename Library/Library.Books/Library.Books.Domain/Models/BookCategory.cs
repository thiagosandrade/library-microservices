using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Books.Domain.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
