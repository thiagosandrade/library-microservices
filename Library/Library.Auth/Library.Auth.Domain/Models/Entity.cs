using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Auth.Domain.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}