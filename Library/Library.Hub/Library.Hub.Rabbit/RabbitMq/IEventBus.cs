using System.Threading.Tasks;
using Library.Hub.Rabbit.Events;

namespace Library.Hub.Rabbit.RabbitMq
{
    public interface IEventBus
    {
        Task PublishMessage<T>(IMessageEvent @event);
        Task Subscribe<T, TH>()
            where T : IMessageEvent, new()
            where TH : IMessageEventHandler<T>;
    }
}
