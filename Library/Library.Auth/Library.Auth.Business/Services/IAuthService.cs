using System.Threading.Tasks;

namespace Library.Auth.Business.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string login, string password);
    }
}