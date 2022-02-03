using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Events;
using Library.Auth.Business.Services;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand, object>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IAuthService _authService;
        private readonly IEventBus _eventBus;

        public CreateUserCommandHandler(IGenericRepository<User> userRepository, IAuthService authService,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _authService = authService;
            _eventBus = eventBus;
        }

        public async Task<object> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.Surname, request.Login, request.Email, request.Password);

            await _userRepository.Create(user);

            var token = await _authService.Authenticate(request.Login, request.Password);

            var @event = new UserLoggedEvent(user);

            await _eventBus.PublishMessage<UserLoggedEvent>(@event);

            return token;
        }
    }
}
