using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorUpdatedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorUpdatedEvent()
        {

        }

        public AuthorUpdatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
