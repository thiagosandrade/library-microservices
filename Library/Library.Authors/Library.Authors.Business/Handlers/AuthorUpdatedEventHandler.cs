using System.Threading.Tasks;
using Library.Authors.Business.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Authors.Business.Handlers
{
    public class AuthorUpdatedEventHandler : IMessageEventHandler<AuthorUpdatedEvent>
    {
        private readonly ILogger<AuthorUpdatedEventHandler> _logger;

        public AuthorUpdatedEventHandler(ILogger<AuthorUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AuthorUpdatedEvent @event)
        {
            _logger.LogInformation("AuthorCreatedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
