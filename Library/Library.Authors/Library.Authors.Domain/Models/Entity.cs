using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Authors.Domain.Models
{
    public class Entity
    {
        [Key]
        public Guid? Id { get; set; }
    }
}