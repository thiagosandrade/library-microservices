using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Authors.Business.Events
{
    public class BookCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public string Name { get; }
        public int Pages { get; }

        public BookCreatedEvent(string name, int pages)
        {
            Name = name;
            Pages = pages;
        }
    }
}
