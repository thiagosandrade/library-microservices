using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Shop.Domain.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; protected set; }
    }
}