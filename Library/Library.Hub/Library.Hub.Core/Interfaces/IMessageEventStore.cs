using System.Collections.ObjectModel;

namespace Library.Hub.Core.Interfaces
{
    public interface IMessageEventStore<T> where T : IMessageEvent
    {
        public void AddMessageEvent(T message);
        public ObservableCollection<T> GetMessageEvents();
    }
}