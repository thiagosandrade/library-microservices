using System;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEvent : IMessageEvent
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Message { get; set; }
        public dynamic Item { get; set; }

        public MessageEvent()
        {

        }

        public MessageEvent(string message, dynamic item = null)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Message = message;
            Item = item;
        }
    }
}