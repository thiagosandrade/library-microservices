using Library.Hub.Rabbit.Events;

namespace Library.Shop.Business.Events
{
    public class CartProductAddedEvent : MessageEvent
    {
        public CartProductAddedEvent()
        {

        }

        public CartProductAddedEvent(string message, dynamic item = null, string[] users = null)
        {
            Message = message;
            Item = item;
            Users = users;
        }
    }
}
