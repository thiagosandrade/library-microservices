using MediatR;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Authors.Business.Events
{
    public class AuthorDeletedEvent : IMessageEvent, IRequest<Unit>
    {
        public dynamic Message { get; set; }

        public AuthorDeletedEvent()
        {

        }

        public AuthorDeletedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
