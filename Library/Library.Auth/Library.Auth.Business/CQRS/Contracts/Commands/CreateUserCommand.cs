using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}