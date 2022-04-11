using Library.Hub.Rabbit.Events;
using MediatR;

namespace Library.Books.Business.Events
{
    public class BookUpdatedEvent : MessageEvent, IRequest<Unit>
    {
        public BookUpdatedEvent()
        {

        }
        public BookUpdatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
