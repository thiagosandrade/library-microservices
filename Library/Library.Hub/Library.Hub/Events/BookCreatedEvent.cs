
using Library.Hub.Rabbit.RabbitMq;

namespace Library.Hub.Events
{
    public class BookCreatedEvent : MessageEvent
    {
        public string Name { get; }
        public int Pages { get; }

        public BookCreatedEvent(string name, int pages)
        {
            Name = name;
            Pages = pages;
        }
    }
}
