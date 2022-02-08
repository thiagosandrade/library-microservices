using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class UpdateUserCommandHandler : BaseHandler, IRequestHandler<UpdateUserCommand>
    {
        private readonly IGenericRepository<User> _userRepository;

        public UpdateUserCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.Surname, request.Login, request.Password, request.Email, request.Id);

            await _userRepository.Update(user);

            return Unit.Value;
        }
    }
}
