using System.Threading.Tasks;
using Library.Hub.Events;
using Library.Hub.Rabbit.Events;
using Microsoft.Extensions.Logging;

namespace Library.Hub.Handlers
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
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
