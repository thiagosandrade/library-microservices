using Dapr;
using Library.Hub.Events;
using Library.Shop.Business.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Shop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationEventController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public IntegrationEventController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [Topic("library-pub-sub", "BookDeletedEvent")]
        [HttpPost("BookDeletedEvent")]
        public async void BookDeletedEvent(BookDeletedEvent @event, [FromServices] BookDeletedEventHandler handler)
        {
            _logger.LogInformation($"BookDeletedEvent consumed: {@event}");

            await handler.Handle(@event);
        }
    }
}
