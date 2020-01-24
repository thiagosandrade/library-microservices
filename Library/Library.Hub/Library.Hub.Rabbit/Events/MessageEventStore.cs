using System.Collections.ObjectModel;
using Library.Hub.Rabbit.Events.Interfaces;

namespace Library.Hub.Rabbit.Events
{
    public class MessageEventStore : IMessageEventStore
    {
        private ObservableCollection<MessageEvent> MessageEvents { get; }

        public MessageEventStore()
        {
            MessageEvents = new ObservableCollection<MessageEvent>();
        }

        public void AddMessageEvent(MessageEvent message)
        {
            MessageEvents.Add(message);
        }

        public ObservableCollection<MessageEvent> GetMessageEvents()
        {
            return MessageEvents;
        }
    }
}
