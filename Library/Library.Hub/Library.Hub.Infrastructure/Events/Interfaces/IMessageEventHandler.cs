using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Events.Interfaces
{
    public interface IMessageEventHandler<in T> where T : IMessageEvent
    {
        Task Handle(T @event);
    }
}