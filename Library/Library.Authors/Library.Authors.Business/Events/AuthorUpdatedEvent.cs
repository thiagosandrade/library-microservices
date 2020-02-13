using MediatR;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Authors.Business.Events
{
    public class AuthorUpdatedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public AuthorUpdatedEvent()
        {

        }

        public AuthorUpdatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
