namespace Library.Hub.Infrastructure.Events.Interfaces
{
    public interface IMessageEvent
    {
        public string[] Users { get; set; }
        public string Message { get; set; }
        public dynamic Item { get; set; }
    }
}