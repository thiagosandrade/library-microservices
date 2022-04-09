using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class BookDeletedEvent : MessageEvent, IRequest<Unit>
    {
        public BookDeletedEvent()
        {

        }

        public BookDeletedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
