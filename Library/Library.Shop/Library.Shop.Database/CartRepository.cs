using System.Linq;
using System.Threading.Tasks;
using Library.Shop.Database.Interfaces;
using Library.Shop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Shop.Database
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CleanItemsFromCartWhenBookDeleted(int id)
        {
            var cartsWithDeletedProd = await _context.Products.Where(x => x.Id == id).ToListAsync();

            _context.RemoveRange(cartsWithDeletedProd);

            await _context.SaveChangesAsync();
        }
    }
}
