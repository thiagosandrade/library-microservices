using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Shop.Business.Events
{
    public class CartProductRemovedEvent : MessageEvent, IRequest<Unit>
    {
        public CartProductRemovedEvent()
        {

        }

        public CartProductRemovedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
