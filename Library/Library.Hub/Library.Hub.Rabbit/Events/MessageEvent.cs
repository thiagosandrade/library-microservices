using System;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEvent : IMessageEvent
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public dynamic Message { get; set; }

        public MessageEvent()
        {

        }

        public MessageEvent(dynamic message)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Message = message;
        }
    }
}