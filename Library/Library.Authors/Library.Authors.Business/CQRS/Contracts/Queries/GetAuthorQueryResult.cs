using System;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAuthorQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int Age => DateTime.Now.Year - Birth.Year;
        public Guid PlaceOfBirthId { get; set; }
        public virtual GetPlaceOfBirthResult PlaceOfBirth { get; set; }
    }
}
