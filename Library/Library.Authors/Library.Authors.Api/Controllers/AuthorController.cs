using System.Threading.Tasks;
using Library.Authors.Business.CQRS.Contracts.Commands;
using Library.Authors.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Authors.Api.Controllers
{
    [ApiController]
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
        public async Task<GetAuthorQueryResult> Get(int id)
        {
            var query = new GetAuthorQuery()
            {
                AuthorId = id
            };

            return await _mediator.Send(query);
        }

        [HttpGet]
        public async Task<GetAllAuthorQueryResult> Get()
        {
            return await _mediator.Send(new GetAllAuthorQuery());
        }

        [HttpPost]
        public async Task Post([FromBody] CreateAuthorCommand command)
        {
            _logger.LogInformation("Command received: {0}", command);

            await _mediator.Send(command);
        }
    }
}
