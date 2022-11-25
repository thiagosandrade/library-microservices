using System.ComponentModel.DataAnnotations;

namespace Library.Books.Domain.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}