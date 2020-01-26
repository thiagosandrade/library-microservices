using MediatR;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Auth.Business.Events
{
    public class UserLoggedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public UserLoggedEvent()
        {

        }

        public UserLoggedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
