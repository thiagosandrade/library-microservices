using Library.Auth.Api.Requests;
using Library.Auth.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Auth.Api.Controllers
{
    [ApiController]
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

        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            return Ok(new
            {
                Result = result
            });

        }
    }
}
