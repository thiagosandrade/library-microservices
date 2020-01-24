using System.Threading.Tasks;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEventHandler : IMessageEventHandler<MessageEvent>
    {
        private readonly ILogger<MessageEventHandler> _logger;
        private readonly IMessageEventStore _messageEventStore;

        public MessageEventHandler(ILogger<MessageEventHandler> logger, IMessageEventStore messageEventStore)
        {
            _logger = logger;
            _messageEventStore = messageEventStore;
        }

        public Task Handle(MessageEvent @event)
        {
            _logger.LogInformation("MessageEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _messageEventStore.AddMessageEvent(@event);
                _logger.LogInformation($"EventId: {@event.Id} - EventMessage: {@event.Message}");
            });
        }
    }
}
