// ---------------------------------------------------------------------------------------
// <copyright file="IMessageEventHandler.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Library.Hub.Rabbit.Events
{
    public interface IMessageEventHandler<in T> where T : IMessageEvent
    {
        Task Handle(T @event);
    }
}