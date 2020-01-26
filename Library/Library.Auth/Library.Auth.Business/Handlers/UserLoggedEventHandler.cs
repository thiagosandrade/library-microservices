using System.Threading.Tasks;
using Library.Auth.Business.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Auth.Business.Handlers
{
    public class UserLoggedEventHandler : IMessageEventHandler<UserLoggedEvent>
    {
        private readonly ILogger<UserLoggedEventHandler> _logger;

        public UserLoggedEventHandler(ILogger<UserLoggedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserLoggedEvent @event)
        {
            _logger.LogInformation("UserLoggedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventMessage: {@event.Message}");
            });
        }
    }
}
