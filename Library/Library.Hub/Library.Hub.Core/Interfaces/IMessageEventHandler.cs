using System.Threading.Tasks;

namespace Library.Hub.Core.Interfaces
{
    public interface IMessageEventHandler<in T> where T : IMessageEvent
    {
        Task Handle(T @event);
    }
}