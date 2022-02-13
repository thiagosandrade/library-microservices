using System;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAuthorQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int Age => DateTime.Now.Year - Birth.Year;
    }
}
