using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Services;
using Library.Auth.Domain.Models;
using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Auth.Business.CQRS.Commands
{
    public class LoginUserCommandHandler : BaseHandler, IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IDaprHandler _daprHandler;
        private readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommandHandler> _logger;

        public LoginUserCommandHandler(IDaprHandler daprHandler, IAuthService authService, ILogger<LoginUserCommandHandler> logger)
        {
            _daprHandler = daprHandler;
            _authService = authService;
            _logger = logger;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.Authenticate(request.Login, request.Password);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Token.Substring(7, token.Token.Length - 7));

            if (token != null)
            {
                var @event = new MessageEvent(message: $"User {request.Login} logged", null, new string[] { jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value });

                _logger.LogInformation($"MessageEvent to publish: {JsonConvert.SerializeObject(@event)}");

                await _daprHandler.PublishMessage<MessageEvent>(@event);
            }

            return token;
        }
    }
}
