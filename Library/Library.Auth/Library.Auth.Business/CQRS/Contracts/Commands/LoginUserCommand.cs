using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Commands
{
    public class LoginUserCommand : IRequest<TokenResponse>
    {
        public LoginUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}