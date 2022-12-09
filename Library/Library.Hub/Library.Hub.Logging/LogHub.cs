using Library.Hub.Core.Interfaces;
using Library.Hub.Logging.Events;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Linq;

namespace Library.Hub.Logging
{
    public class LogHub : ILogHub
    {
        private readonly IMessageEventStore<LogMessageEvent> _messageEventStore;
        private readonly ILogger<LogHub> _logger;

        public LogHub(IMessageEventStore<LogMessageEvent> messageEventStore, ILogger<LogHub> logger)
        {
            _messageEventStore = messageEventStore;
            _logger = logger;

            _logger.LogInformation("LogHub - Collection listening");
            _messageEventStore.GetMessageEvents().CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _logger.LogInformation("LogHub - Collection changed");

            var message = _messageEventStore.GetMessageEvents().Last();

            //Log to Elastic
            if (message.LogLevel.Equals(LogLevel.Information))
                _logger.LogInformation(exception: message.Exception, message: $"{message.BusinessKey} - {message.Message}");
            else if (message.LogLevel.Equals(LogLevel.Warning))
                _logger.LogWarning(exception: message.Exception, message: $"{message.BusinessKey} - {message.Message}");
            else if (message.LogLevel.Equals(LogLevel.Error))
                _logger.LogError(exception: message.Exception, message: $"{message.BusinessKey} - {message.Message}");
        }
    }
}