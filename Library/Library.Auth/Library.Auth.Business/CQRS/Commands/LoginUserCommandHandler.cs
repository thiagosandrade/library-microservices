using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Services;
using Library.Auth.Domain.Models;
using Library.Hub.Infrastructure.Events;
using Library.Hub.Infrastructure.Handlers;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class LoginUserCommandHandler : BaseHandler, IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IDaprHandler _daprHandler;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IDaprHandler daprHandler, IAuthService authService)
        {
            _daprHandler = daprHandler;
            _authService = authService;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.Authenticate(request.Login, request.Password);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Token.Substring(7, token.Token.Length - 7));

            if (token != null)
            {
                var @event = new MessageEvent(message: $"User {request.Login} logged", null, new string[] { jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value });
            
                await _daprHandler.PublishMessage<MessageEvent>(@event);
            }

            return token;
        }
    }
}
