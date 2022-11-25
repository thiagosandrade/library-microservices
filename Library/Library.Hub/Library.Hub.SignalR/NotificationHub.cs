using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Library.Hub.Core.Interfaces;
using Library.Hub.Core.SignalR.Library.Hub.Core.SignalR;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Hub.SignalR
{
    public class NotificationHub : Hub<SignalRHub>, INotificationHub
    {
        private readonly IMessageEventStore _messageEventStore;
        private readonly ILogger<NotificationHub> _logger;

        private readonly IHubContext<SignalRHub, ISignalRHub> _hubContext;

        public NotificationHub(IMessageEventStore messageEventStore, ILogger<NotificationHub> logger, IHubContext<SignalRHub, ISignalRHub> hubContext)
        {
            _messageEventStore = messageEventStore;
            _logger = logger;
            _hubContext = hubContext;

            _logger.LogInformation("NotificationHub - Collection listening");
            _messageEventStore.GetMessageEvents().CollectionChanged += OnCollectionChanged;
        }

        public async Task SendMessage(SignalRMessage message)
        {
            _logger.LogInformation("NotificationHub - Send Message");

            await _hubContext.Clients.All.BroadcastMessage(message);
        }
        
        private async void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _logger.LogInformation("NotificationHub - Collection changed");

            var message = _messageEventStore.GetMessageEvents().Last();

            _logger.LogInformation("NotificationHub - Message date: {0}", message.CreationDate.ToString());

            await _hubContext.Clients.All.BroadcastMessage(new SignalRMessage
            {
                Payload = JsonConvert.SerializeObject(message),
                Type = ""
            });
        }
    }
}
