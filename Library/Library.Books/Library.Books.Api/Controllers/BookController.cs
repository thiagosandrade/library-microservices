using System;
using System.Threading.Tasks;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookQueryResult>> Get(int id)
        {
            var query = new GetBookQuery()
            {
                BookId = id
            };

            var result =  await _mediator.Send(query);

            return Ok(new OkObjectResult(result));

        }

        [HttpGet]
        public async Task<ActionResult<GetAllBookQueryResult>> Get()
        {
            var result = await _mediator.Send(new GetAllBookQuery());

            return Ok(new OkObjectResult(result.Books));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
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
        public async Task<IActionResult> Put([FromBody] UpdateBookCommand command)
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

                var command = new DeleteBookCommand()
                {
                    BookId = id
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
