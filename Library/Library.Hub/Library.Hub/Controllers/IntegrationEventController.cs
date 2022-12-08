using Dapr;
using Library.Hub.Infrastructure.Events;
using Library.Hub.Infrastructure.Handlers;
using Library.Hub.Infrastructure.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            _logger.LogInformation($"MessageEvent consumed: {JsonConvert.SerializeObject(@event)}");
            
            await handler.Handle(@event);
        }

        [Topic("library-pub-sub", "LogMessageEvent")]
        [HttpPost("LogMessageEvent")]
        public async void MessageEvent(LogMessageEvent @event, [FromServices] LogMessageEventHandler handler)
        {
            _logger.LogInformation($"MessageEvent consumed: {JsonConvert.SerializeObject(@event)}");

            await handler.Handle(@event);
        }
    }
}
