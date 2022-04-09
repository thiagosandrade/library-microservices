using System.Threading.Tasks;
using Library.Books.Business.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Books.Business.Handlers
{
    public class BookUpdatedEventHandler : IMessageEventHandler<BookCreatedEvent>
    {
        private readonly ILogger<BookUpdatedEventHandler> _logger;

        public BookUpdatedEventHandler(ILogger<BookUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BookCreatedEvent @event)
        {
            _logger.LogInformation("BookUpdatedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
