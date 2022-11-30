using Library.Hub.Rabbit.Events;
using MediatR;

namespace Library.Books.Business.Events
{
    public class BookUpdatedEvent : MessageEvent, IRequest<Unit>
    {
        public BookUpdatedEvent()
        {

        }
        public BookUpdatedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
