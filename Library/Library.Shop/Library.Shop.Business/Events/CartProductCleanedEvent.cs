using Library.Hub.Rabbit.Events;

namespace Library.Shop.Business.Events
{
    public class CartProductCleanedEvent : MessageEvent
    {
        public CartProductCleanedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
