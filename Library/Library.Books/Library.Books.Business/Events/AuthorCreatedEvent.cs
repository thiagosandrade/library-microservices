using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorCreatedEvent()
        {

        }

        public AuthorCreatedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
