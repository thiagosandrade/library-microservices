using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Interfaces
{
    public interface IMessageEventHandler<in T> where T : IMessageEvent
    {
        Task Handle(T @event);
    }
}