using System;

namespace Library.Authors.Domain.Models
{
    public class Author : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }
        public int Age => DateTime.Now.Year - Birth.Year;
        public int PlaceOfBirthId { get; private set; }
        public virtual PlaceOfBirth PlaceOfBirth { get; private set; }

        public Author(int id, string name, string surname, DateTime birth, int placeOfBirthId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
            PlaceOfBirthId = placeOfBirthId;
        }
    }
}
