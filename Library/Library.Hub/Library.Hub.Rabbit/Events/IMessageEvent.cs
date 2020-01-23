// ---------------------------------------------------------------------------------------
// <copyright file="IMessageEvent.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Library.Hub.Rabbit.Events
{
    public interface IMessageEvent
    {
        public dynamic Message { get; set; }
    }
}