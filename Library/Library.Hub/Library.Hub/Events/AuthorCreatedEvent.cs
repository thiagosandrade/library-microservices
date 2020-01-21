using Library.Hub.Rabbit.RabbitMq;

namespace Library.Hub.Events
{
    public class AuthorCreatedEvent : MessageEvent
    {
        public string Name { get; }
        public int Surname { get; }

        public AuthorCreatedEvent(string name, int surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
