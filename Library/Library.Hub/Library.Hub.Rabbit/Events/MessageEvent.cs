using System;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEvent : IMessageEvent
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string[] Users { get; set; }
        public string Message { get; set; }
        public dynamic Item { get; set; }

        public MessageEvent()
        {

        }

        public MessageEvent(string message, dynamic item = null, string[] users = null)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Users = users;
            Message = message;
            Item = item;
        }
    }
}