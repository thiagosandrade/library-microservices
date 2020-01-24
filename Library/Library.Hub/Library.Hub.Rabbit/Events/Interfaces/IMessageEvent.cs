namespace Library.Hub.Rabbit.Events.Interfaces
{
    public interface IMessageEvent
    {
        public dynamic Message { get; set; }
    }
}