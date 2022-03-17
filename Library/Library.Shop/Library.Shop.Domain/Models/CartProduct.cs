using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Shop.Domain.Models
{
    public class CartProduct : Entity
    {
        public CartProduct(int productId, int cartId, int id = 0)
        {
            Id = id;
            ProductId = productId;
            CartId = cartId;
        }

        public int CartId { get; private set; }
        public int ProductId { get; private set; }
    }
}
