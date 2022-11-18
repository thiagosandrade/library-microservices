namespace Library.Shop.Domain.Models
{
    public class CartProduct : Entity
    {
        public CartProduct(int productId, int cartId, int quantity, int id = 0)
        {
            Id = id;
            ProductId = productId;
            CartId = cartId;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            Quantity -= quantity;
        }

        public int CartId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}
