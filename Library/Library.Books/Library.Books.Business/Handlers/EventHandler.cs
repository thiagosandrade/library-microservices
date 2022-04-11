using System.Threading.Tasks;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Books.Business.Handlers
{
    public class EventHandler<T> : IMessageEventHandler<T> where T : IMessageEvent
    {
        private readonly ILogger<EventHandler<T>> _logger;

        public EventHandler(ILogger<EventHandler<T>> logger)
        {
            _logger = logger;
        }

        public Task Handle(T @event)
        {
            _logger.LogInformation($"{nameof(EventHandler<T>)} {@event}");

            return Task.Run(() =>
            {
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
