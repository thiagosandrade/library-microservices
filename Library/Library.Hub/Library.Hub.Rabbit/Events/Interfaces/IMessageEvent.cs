namespace Library.Hub.Rabbit.Events.Interfaces
{
    public interface IMessageEvent
    {
        public string Message { get; set; }
        public dynamic Item { get; set; }
    }
}