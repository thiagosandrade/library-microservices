using Library.Hub.Core.SignalR.Library.Hub.Core.SignalR;
using System.Threading.Tasks;

namespace Library.Hub.SignalR
{
    public interface INotificationHub
    {
        Task SendMessage(SignalRMessage message);
    }
}