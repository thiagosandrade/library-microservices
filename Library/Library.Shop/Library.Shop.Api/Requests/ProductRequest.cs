namespace Library.Shop.Api.Requests
{
    public class ProductRequest
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}