using Library.Hub.Rabbit.Events;
using MediatR;

namespace Library.Books.Business.Events
{
    public class BookCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public BookCreatedEvent()
        {

        }
        public BookCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
