using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEventHandler : IMessageEventHandler<MessageEvent>
    {
        private readonly ILogger<MessageEventHandler> _logger;

        public MessageEventHandler(ILogger<MessageEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(MessageEvent @event)
        {
            _logger.LogInformation("MessageEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventId: {@event.Id} - EventMessage: {@event.Message}");
            });
        }
    }
}
