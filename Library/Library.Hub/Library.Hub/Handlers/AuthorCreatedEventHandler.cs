using System.Threading.Tasks;
using Library.Hub.Events;
using Library.Hub.Rabbit.Events;
using Microsoft.Extensions.Logging;

namespace Library.Hub.Handlers
{
    public class AuthorCreatedEventHandler : IMessageEventHandler<AuthorCreatedEvent>
    {
        private readonly ILogger<AuthorCreatedEventHandler> _logger;

        public AuthorCreatedEventHandler(ILogger<AuthorCreatedEventHandler> logger)
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
