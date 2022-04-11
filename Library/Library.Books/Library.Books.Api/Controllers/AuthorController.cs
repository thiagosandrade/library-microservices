using System;
using System.Threading.Tasks;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Books.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Reader, SuperUser")]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAuthorQueryResult>> Get(int id)
        {
            var query = new GetAuthorQuery()
            {
                AuthorId = id
            };

            var result = await _mediator.Send(query);

            return Ok(new OkObjectResult(result));
        }

        [HttpGet]
        public async Task<ActionResult<GetAllAuthorQueryResult>> Get()
        {
            var result = await _mediator.Send(new GetAllAuthorQuery());

            return Ok(new OkObjectResult(result.Authors));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAuthorCommand command)
        {
            try
            {
                _logger.LogInformation($"Command received: {command}");

                await _mediator.Send(command);

                return Ok(new OkObjectResult("Command Completed"));
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.ToString() });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAuthorCommand command)
        {
            try
            {
                _logger.LogInformation($"Command received: {command}");

                await _mediator.Send(command);

                return Ok(new OkObjectResult("Command Completed"));
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.ToString() });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Command received: {id}");

                var command = new DeleteAuthorCommand()
                {
                    AuthorId = id
                };

                await _mediator.Send(command);

                return Ok(new OkObjectResult("Command Completed"));
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.ToString() });
            }
        }
    }
}
