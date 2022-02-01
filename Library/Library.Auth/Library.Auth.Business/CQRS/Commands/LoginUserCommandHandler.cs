using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Events;
using Library.Auth.Business.Services;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class LoginUserCommandHandler : BaseHandler, IRequestHandler<LoginUserCommand, string>
    {
        private readonly IEventBus _eventBus;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IEventBus eventBus, IAuthService authService)
        {
            _eventBus = eventBus;
            _authService = authService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.Authenticate(request.Login, request.Password);

            if(token != null)
            {
                var @event = new UserLoggedEvent($"User {request.Login} logged");

                await _eventBus.PublishMessage<UserLoggedEvent>(@event);
            }

            return token;
        }
    }
}
