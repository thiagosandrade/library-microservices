using System.Threading.Tasks;
using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Hub.Infrastructure.Handlers
{
    public class MessageEventHandler : IMessageEventHandler<MessageEvent>
    {
        private readonly ILogger<MessageEventHandler> _logger;
        private readonly IMessageEventStore<MessageEvent> _messageEventStore;

        public MessageEventHandler(ILogger<MessageEventHandler> logger, IMessageEventStore<MessageEvent> messageEventStore)
        {
            _logger = logger;
            _messageEventStore = messageEventStore;
        }

        public Task Handle(MessageEvent @event)
        {
            _logger.LogInformation("MessageEventHandler {0}", JsonConvert.SerializeObject(@event));
            return Task.Run(() =>
            {
                _messageEventStore.AddMessageEvent(@event);
                _logger.LogInformation($"EventId: {@event.Id} - EventMessage: {@event.Message}");
            });
        }
    }
}
