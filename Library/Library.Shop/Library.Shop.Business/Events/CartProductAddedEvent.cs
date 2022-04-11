using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Shop.Business.Events
{
    public class CartProductAddedEvent : MessageEvent, IRequest<Unit>
    {
        public CartProductAddedEvent()
        {

        }

        public CartProductAddedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
