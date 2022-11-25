using System;
using System.Threading.Tasks;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Rabbit.RabbitMq
{
    public interface IEventBus : IDisposable
    {
        Task PublishMessage<T>(IMessageEvent @event);
        Task Subscribe<T, TH>()
            where T : IMessageEvent, new()
            where TH : IMessageEventHandler<T>;
    }
}
