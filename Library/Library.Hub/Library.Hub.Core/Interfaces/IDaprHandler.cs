using System.Threading.Tasks;

namespace Library.Hub.Core.Interfaces
{
    public interface IDaprHandler
    {
        Task PublishMessage<T>(T message);
    }
}