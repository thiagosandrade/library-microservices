using System.Threading.Tasks;
using Library.Books.Business.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Books.Business.Handlers
{
    public class AuthorCreatedEventHandler : IMessageEventHandler<AuthorCreatedEvent>
    {
        private readonly ILogger<BookCreatedEventHandler> _logger;

        public AuthorCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AuthorCreatedEvent @event)
        {
            _logger.LogInformation("AuthorCreatedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
