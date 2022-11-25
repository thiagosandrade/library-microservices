using System.Threading.Tasks;

namespace Library.Shop.Database.Interfaces
{
    public interface ICartRepository
    {
        Task CleanItemsFromCartWhenBookDeleted(int id);
    }
}