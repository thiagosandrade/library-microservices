using Library.Hub.Rabbit.Events;

namespace Library.Hub.Events
{
    public class BookCreatedEvent : MessageEvent
    {
        public BookCreatedEvent(string message, dynamic item = null)
        {
            Message = message;
            Item = item;
        }
    }
}
