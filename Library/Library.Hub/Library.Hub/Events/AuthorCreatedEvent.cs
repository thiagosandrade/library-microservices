using Library.Hub.Rabbit.Events;

namespace Library.Hub.Events
{
    public class AuthorCreatedEvent : IMessageEvent
    {
        public dynamic Message { get; set; }

        public AuthorCreatedEvent()
        {

        }

        public AuthorCreatedEvent(dynamic message)
        {
            Message = message;
        }

    }
}
