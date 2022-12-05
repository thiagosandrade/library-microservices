using System.Collections.ObjectModel;

namespace Library.Hub.Infrastructure.Events.Interfaces
{
    public interface IMessageEventStore
    {
        public void AddMessageEvent(MessageEvent message);
        public ObservableCollection<MessageEvent> GetMessageEvents();
    }
}