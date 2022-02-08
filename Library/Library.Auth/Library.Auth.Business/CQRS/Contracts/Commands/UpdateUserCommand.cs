using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(string name, string surname, string login, string email, string password, int id)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Login = login;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}