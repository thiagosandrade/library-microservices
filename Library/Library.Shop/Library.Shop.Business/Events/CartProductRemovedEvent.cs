using Library.Hub.Rabbit.Events;

namespace Library.Shop.Business.Events
{
    public class CartProductRemovedEvent : MessageEvent
    {
        public CartProductRemovedEvent()
        {

        }
        public CartProductRemovedEvent(string message, dynamic item = null)
        {
            Message = message;
            Item = item;
        }
    }
}
