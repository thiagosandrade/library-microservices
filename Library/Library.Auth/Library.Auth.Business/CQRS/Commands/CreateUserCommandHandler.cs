using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand>
    {
        private readonly IGenericRepository<User> _userRepository;

        public CreateUserCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.Surname, request.Login, request.Password, request.Email);

            await _userRepository.Create(user);

            return Unit.Value;
        }
    }
}
