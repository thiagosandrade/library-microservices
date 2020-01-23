// ---------------------------------------------------------------------------------------
// <copyright file="MessageEvent.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEvent : IMessageEvent
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public dynamic Message { get; set; }

        public MessageEvent(dynamic message)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Message = message;
        }
    }
}