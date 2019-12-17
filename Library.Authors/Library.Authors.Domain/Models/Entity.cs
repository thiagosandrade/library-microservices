using System.ComponentModel.DataAnnotations;

namespace Library.Authors.Domain.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}