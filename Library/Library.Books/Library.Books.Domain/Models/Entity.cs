using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Books.Domain.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}