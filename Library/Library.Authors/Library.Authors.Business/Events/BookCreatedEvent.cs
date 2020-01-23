using Library.Hub.Rabbit.Events;
using MediatR;

namespace Library.Authors.Business.Events
{
    public class BookCreatedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public BookCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
