using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Events;
using Library.Auth.Business.Services;
using Library.Auth.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class LoginUserCommandHandler : BaseHandler, IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IEventBus _eventBus;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IEventBus eventBus, IAuthService authService)
        {
            _eventBus = eventBus;
            _authService = authService;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.Authenticate(request.Login, request.Password);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Token.Substring(7, token.Token.Length - 7));

            if (token != null)
            {
                var @event = new UserLoggedEvent(message: $"User {request.Login} logged", null, new string[] { jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value });
            

                await _eventBus.PublishMessage<UserLoggedEvent>(@event);
            }

            return token;
        }
    }
}
