using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Library.Hub.Rabbit.Events.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Library.Hub.Controllers
{
    public class NotificationHub : DynamicHub
    {
        private readonly IMessageEventStore _messageEventStore;

        public NotificationHub(IMessageEventStore messageEventStore)
        {
            _messageEventStore = messageEventStore;
            messageEventStore.GetMessageEvents().CollectionChanged += OnCollectionChanged;
        }

        private async void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User?.Identity?.Name, e);
        }

        public async Task SendMessage(string message)
        {
            var avatar = Context.User?.Claims?.FirstOrDefault(c => c.Type == "avatar")?.Value;

            await Clients.All.SendAsync("ReceiveMessage", Context.User?.Identity?.Name, avatar, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("ReceiveMessage", Context.User?.Identity?.Name, _messageEventStore.GetMessageEvents());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("ReceiveNotification", $"{Context.User?.Identity?.Name} disconnected");
        }
    }
}
