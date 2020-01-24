using MediatR;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Authors.Business.Events
{
    public class AuthorCreatedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public AuthorCreatedEvent()
        {

        }

        public AuthorCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
