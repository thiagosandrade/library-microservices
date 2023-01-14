
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Shop.Domain.Models
{
    public class Cart : Entity
    {
        public Cart(int userId, int id = 0)
        {
            Id = id;
            UserId = userId;
            CreatedDate = DateTime.Now;
        }

        public int UserId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public virtual IList<CartProduct> Items { get; private set; }
        
        public void AddItem(int productId, int quantity)
        {
            var product = Items.FirstOrDefault(x => x.ProductId.Equals(productId));

            if (product == null)
                Items.Add(new CartProduct(productId, Id, quantity));
            else
                product.IncreaseQuantity(quantity);
        }

        public void RemoveItem(int productId, int quantity)
        {
            var product = Items.FirstOrDefault(x => x.ProductId.Equals(productId));

            product?.RemoveQuantity(quantity);

            if (product.Quantity == 0)
                Items.Remove(product);
        }
    }
}
