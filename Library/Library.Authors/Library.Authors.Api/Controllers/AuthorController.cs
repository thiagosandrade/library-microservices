using System.Threading.Tasks;
using Library.Authors.Api.Dto;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Business.Events;
using Library.Authors.Rabbit.RabbitMq;
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
        private readonly IEventBus _eventBus;

        public AuthorController(ILogger<AuthorController> logger, IMediator mediator, IEventBus eventBus)
        {
            _logger = logger;
            _mediator = mediator;
            _eventBus = eventBus;
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
        public async Task Post([FromBody] BookCreated book)
        {
            _logger.LogInformation("Message received");
            var @event = new BookCreatedEvent(book.Name, book.Pages);

            await _eventBus.PublishMessage<BookCreatedEvent>(@event);
        }
    }
}
