using MediatR;
using Library.Hub.Rabbit.Events;

namespace Library.Authors.Business.Events
{
    public class AuthorCreatedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public AuthorCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
