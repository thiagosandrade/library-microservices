using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Books.Business.Events
{
    public class AuthorDeletedEvent : MessageEvent, IRequest<Unit>
    {
        public AuthorDeletedEvent()
        {

        }

        public AuthorDeletedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
