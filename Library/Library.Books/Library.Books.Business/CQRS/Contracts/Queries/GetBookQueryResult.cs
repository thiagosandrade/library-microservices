namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetBookQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public int CategoryId { get; set; }
        public GetCategoryResult Category { get; set; }
    }
}
