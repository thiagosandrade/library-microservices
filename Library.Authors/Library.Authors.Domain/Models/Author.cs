using System;

namespace Library.Authors.Domain.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int Age => DateTime.Now.Year - Birth.Year;
        public int PlaceOfBirthId { get; set; }
        public virtual PlaceOfBirth PlaceOfBirth { get; set; }
    }
}
