using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Interfaces
{
    public interface IDaprHandler
    {
        Task PublishMessage<T>(T message);
    }
}