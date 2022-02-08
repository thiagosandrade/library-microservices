using Library.Auth.Api.Requests;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Roles = "Reader, SuperUser")]
        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation($"Get Users");

            var result = await _mediator.Send(new GetAllUserQuery());

            return Ok(new OkObjectResult(result.Users));

        }

        [Authorize(Roles = "Reader, SuperUser")]
        [HttpGet("WithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            _logger.LogInformation($"Get Users with Roles");

            var result = await _mediator.Send(new GetAllUserWithRolesQuery());

            return Ok(new OkObjectResult(result.Users));

        }

        [Authorize(Roles = "SuperUser")]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateRequest user)
        {
            _logger.LogInformation($"Creating user: {user}");

            await _mediator.Send(new CreateUserCommand( user.Name,
                                                        user.Surname,
                                                        user.Login,
                                                        user.Email,
                                                        user.Password));

            return Ok();

        }

        [Authorize(Roles = "SuperUser")]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateRequest user)
        {
            _logger.LogInformation($"Creating user: {user}");

            await _mediator.Send(new UpdateUserCommand(user.Name,
                                                        user.Surname,
                                                        user.Login,
                                                        user.Email,
                                                        user.Password,
                                                        user.Id));

            return Ok();

        }
    }
}
