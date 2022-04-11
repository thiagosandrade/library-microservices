using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorDeletedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorDeletedEvent()
        {

        }

        public AuthorDeletedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
