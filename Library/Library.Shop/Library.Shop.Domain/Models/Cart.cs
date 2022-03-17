
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

        public virtual List<CartProduct> Items { get; private set; }
        
        public void AddItem(int productId)
        {
            Items.Add(new CartProduct(productId, Id));
        }

        public void RemoveItem(int productId)
        {
            var product = Items.FirstOrDefault(x => x.ProductId.Equals(productId));

            if(product != null)
                Items.Remove(product);
        }
    }
}
