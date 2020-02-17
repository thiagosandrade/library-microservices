using Library.Hub.Core.SignalR.Library.Hub.Core.SignalR;
using System.Threading.Tasks;

namespace Library.Hub.Core.Interfaces
{
    public interface ISignalRHub
    {
        Task BroadcastMessage(SignalRMessage message);
    }
}