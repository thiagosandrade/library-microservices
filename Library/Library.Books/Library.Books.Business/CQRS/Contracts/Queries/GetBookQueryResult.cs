using System;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetBookQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public Guid CategoryId { get; set; }
        public GetCategoryResult Category { get; set; }
    }
}
