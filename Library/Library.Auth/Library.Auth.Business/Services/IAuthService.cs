using Library.Auth.Domain.Models;
using System.Threading.Tasks;

namespace Library.Auth.Business.Services
{
    public interface IAuthService
    {
        Task<TokenResponse> Authenticate(string login, string password);
    }
}