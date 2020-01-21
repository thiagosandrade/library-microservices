using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Library.Hub.Controllers
{
    public class NotificationHub : DynamicHub
    {
        public async Task SendMessage(string message)
        {
            var avatar = Context.User?.Claims?.FirstOrDefault(c => c.Type == "avatar")?.Value;

            await Clients.All.SendAsync("ReceiveMessage", Context.User?.Identity?.Name, avatar, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("ReceiveNotification", $"{Context.User?.Identity?.Name} join chat");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("ReceiveNotification", $"{Context.User?.Identity?.Name} left chat");
        }
    }
}
