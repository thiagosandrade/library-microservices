using Library.Auth.Api.Requests;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
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

            if (token is null)
                return BadRequest(new {message = "Username or password incorrect"});

            return Ok(new OkObjectResult(token));

        }

        [Authorize(Roles = "Reader, SuperUser")]
        [HttpGet("GetUserLogged")]
        public async Task<IActionResult> GetUserLogged()
        {
            _logger.LogInformation($"Get user details");

            var accessToken = HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(accessToken))
                return BadRequest(new { message = "Invalid token" });

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            var userDetails = await _mediator.Send(new GetUserQuery(email));

            userDetails.Token = accessToken;

            return Ok(new OkObjectResult(userDetails));

        }
    }
}
