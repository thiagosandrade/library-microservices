using Library.Hub.Rabbit.Events;

namespace Library.Auth.Business.Events
{
    public class UserLoggedEvent : MessageEvent
    {
        public UserLoggedEvent()
        {

        }

        public UserLoggedEvent(string message, dynamic item = null)
        {
            Message = message;
            Item = item;
        }
    }
}
