using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorUpdatedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorUpdatedEvent()
        {

        }

        public AuthorUpdatedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
