using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Books.Domain.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
