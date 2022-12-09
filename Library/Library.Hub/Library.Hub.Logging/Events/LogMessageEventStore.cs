using System.Collections.ObjectModel;
using Library.Hub.Core.Interfaces;

namespace Library.Hub.Logging.Events
{
    public class LogMessageEventStore : IMessageEventStore<LogMessageEvent>
    {
        private ObservableCollection<LogMessageEvent> MessageEvents { get; }

        public LogMessageEventStore()
        {
            MessageEvents = new ObservableCollection<LogMessageEvent>();
        }

        public void AddMessageEvent(LogMessageEvent message)
        {
            MessageEvents.Add(message);
        }

        public ObservableCollection<LogMessageEvent> GetMessageEvents()
        {
            return MessageEvents;
        }
    }
}
