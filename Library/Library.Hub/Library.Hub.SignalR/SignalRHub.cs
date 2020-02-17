using Library.Hub.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Library.Hub.SignalR
{
    public class SignalRHub : Hub<ISignalRHub>
    {
    }
}
