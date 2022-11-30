using Library.Hub.Rabbit.Events;

namespace Library.Hub.Events
{
    public class BookDeletedEvent : MessageEvent
    {
        public BookDeletedEvent()
        {

        }
        public BookDeletedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
