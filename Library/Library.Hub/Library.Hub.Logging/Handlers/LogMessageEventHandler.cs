using Library.Hub.Core.Interfaces;
using Library.Hub.Logging.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Library.Hub.Logging.Handlers
{
    public class LogMessageEventHandler : IMessageEventHandler<LogMessageEvent>
    {
        private readonly ILogger<LogMessageEventHandler> _logger;
        private readonly IMessageEventStore<LogMessageEvent> _messageEventStore;

        public LogMessageEventHandler(ILogger<LogMessageEventHandler> logger, IMessageEventStore<LogMessageEvent> messageEventStore)
        {
            _logger = logger;
            _messageEventStore = messageEventStore;
        }

        public Task Handle(LogMessageEvent @event)
        {
            _logger.LogInformation("LogMessageEventHandler {0}", JsonConvert.SerializeObject(@event));
            return Task.Run(() =>
            {
                _messageEventStore.AddMessageEvent(@event);
                _logger.LogInformation($"BusinessKey: {@event.BusinessKey} - EventMessage: {@event.Message}");
            });
        }
    }
}
