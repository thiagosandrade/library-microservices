using System;
using System.Threading.Tasks;

namespace Library.Authors.Rabbit.RabbitMq
{
    public interface IEventBus
    {
        Task PublishMessage<T>(MessageEvent @event);
        Task Subscribe<T, TH>()
            where T : MessageEvent
            where TH : IMessageEventHandler<T>;
    }

    public interface IMessageEventHandler<in T> where T : MessageEvent
    {
        Task Handle(T @event);
    }

    public class MessageEvent
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
