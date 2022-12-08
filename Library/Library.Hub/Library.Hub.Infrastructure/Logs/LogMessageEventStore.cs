using System.Collections.ObjectModel;
using Library.Hub.Infrastructure.Interfaces;

namespace Library.Hub.Infrastructure.Logs
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
