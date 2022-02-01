using Library.Auth.Api.Requests;
using Library.Auth.Business.CQRS.Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest user)
        {
            _logger.LogInformation($"Validating user: {user}");

            var token = await _mediator.Send(new LoginUserCommand(user.Login,
                                                                  user.Password));

            if (string.IsNullOrEmpty(token))
                return BadRequest(new {message = "Username or password incorrect"});

            return Ok(new
            {
                Token = token
            });

        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateRequest user)
        {
            _logger.LogInformation($"Creating user: {user}");

            var token = await _mediator.Send(new CreateUserCommand(user.Name,
                                                                   user.Surname,
                                                                   user.Login,
                                                                   user.Login,
                                                                   user.Password));

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Creation of user not succeeded" });

            return Ok(new
            {
                Token = token
            });

        }
    }
}
