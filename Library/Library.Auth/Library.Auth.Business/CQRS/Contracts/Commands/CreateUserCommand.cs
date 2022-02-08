using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Commands
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string name, string surname, string login, string email, string password)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}