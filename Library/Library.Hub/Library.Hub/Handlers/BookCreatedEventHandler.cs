using Library.Hub.Rabbit.RabbitMq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Hub.Events
{
    public class BookCreatedEventHandler : IMessageEventHandler<BookCreatedEvent>
    {
        private readonly ILogger<BookCreatedEventHandler> _logger;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BookCreatedEvent @event)
        {
            _logger.LogInformation("BookCreatedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventId: {@event.Id} - EventName: {@event.Name}");
            });
        }
    }
}
