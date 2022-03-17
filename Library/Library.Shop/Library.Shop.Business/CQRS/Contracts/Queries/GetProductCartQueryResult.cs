
namespace Library.Shop.Business.CQRS.Contracts.Queries
{
    public class GetProductCartQueryResult
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
     
    }
}