using System.Threading.Tasks;

namespace Library.Auth.Business.Services
{
    public interface IAuthService
    {
        Task<object> Authenticate(string login, string password);
    }
}