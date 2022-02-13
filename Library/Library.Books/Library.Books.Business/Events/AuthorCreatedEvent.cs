using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorCreatedEvent()
        {

        }

        public AuthorCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
