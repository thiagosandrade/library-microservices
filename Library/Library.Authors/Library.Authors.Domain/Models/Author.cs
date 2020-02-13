using System;

namespace Library.Authors.Domain.Models
{
    public class Author : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }
        public int Age => DateTime.Now.Year - Birth.Year;
        public Guid PlaceOfBirthId { get; private set; }
        public virtual PlaceOfBirth PlaceOfBirth { get; private set; }

        protected Author()
        {

        }

        public Author(string name, string surname, DateTime birth, Guid? placeOfBirthId = null, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            Surname = surname;
            Birth = birth;
            PlaceOfBirthId = placeOfBirthId ?? Guid.NewGuid();
        }
    }
}
