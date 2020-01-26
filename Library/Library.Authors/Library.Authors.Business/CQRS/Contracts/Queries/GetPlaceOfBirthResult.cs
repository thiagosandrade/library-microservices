using System;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetPlaceOfBirthResult
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}