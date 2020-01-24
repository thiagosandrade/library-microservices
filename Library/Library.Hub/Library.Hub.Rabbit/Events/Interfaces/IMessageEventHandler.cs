using System.Threading.Tasks;

namespace Library.Hub.Rabbit.Events.Interfaces
{
    public interface IMessageEventHandler<in T> where T : IMessageEvent
    {
        Task Handle(T @event);
    }
}