using Library.Hub.Rabbit.Events;

namespace Library.Hub.Events
{
    public class AuthorCreatedEvent : MessageEvent
    {
        public AuthorCreatedEvent(string message, dynamic item = null)
        {
            Message = message;
            Item = item;
        }
    }
}
