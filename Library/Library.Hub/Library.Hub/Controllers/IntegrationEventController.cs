using Dapr;
using Library.Hub.Infrastructure.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private readonly ILogger<IntegrationEventController> _logger;

        public IntegrationEventController(ILogger<IntegrationEventController> logger)
        {
            _logger = logger;
        }

        [Topic("library-pub-sub", "MessageEvent")]
        [HttpPost("MessageEvent")]
        public async void MessageEvent(MessageEvent @event, [FromServices] MessageEventHandler handler)
        {
            _logger.LogInformation($"MessageEvent consumed: {@event}");
            
            await handler.Handle(@event);
        }
    }
}
