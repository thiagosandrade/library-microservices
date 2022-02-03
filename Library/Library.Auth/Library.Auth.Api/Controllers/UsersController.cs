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
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation($"Get Users");

            var result = await _mediator.Send(new GetAllUserQuery());

            return Ok(new
            {
                Result = result
            });

        }

        [Authorize(Roles = "SuperUser")]
        [HttpGet("UsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            _logger.LogInformation($"Get Users with Roles");

            var result = await _mediator.Send(new GetAllUserWithRolesQuery());

            return Ok(new
            {
                Result = result
            });

        }
    }
}
