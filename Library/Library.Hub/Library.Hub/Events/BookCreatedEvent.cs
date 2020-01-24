﻿using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Events
{
    public class BookCreatedEvent : IMessageEvent
    {
        public dynamic Message { get; set; }

        public BookCreatedEvent()
        {
            
        }

        public BookCreatedEvent(dynamic message)
        {
            Message = message;
        }
    }
}
