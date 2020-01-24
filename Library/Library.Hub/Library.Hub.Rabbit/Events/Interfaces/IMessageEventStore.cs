using System.Collections.ObjectModel;

namespace Library.Hub.Rabbit.Events.Interfaces
{
    public interface IMessageEventStore
    {
        public void AddMessageEvent(MessageEvent message);
        public ObservableCollection<MessageEvent> GetMessageEvents();
    }
}