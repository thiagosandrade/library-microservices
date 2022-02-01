using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Commands
{
    public class LoginUserCommand : IRequest<string>
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