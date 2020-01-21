using Library.Hub.Rabbit.RabbitMq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Hub.Events
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
                _logger.LogInformation($"EventId: {@event.Id} - EventName: {@event.Name}");
            });
        }
    }
}
