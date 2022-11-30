using Library.Hub.Rabbit.Events;
using MediatR;

namespace Library.Books.Business.Events
{
    public class BookCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public BookCreatedEvent()
        {

        }
        public BookCreatedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
