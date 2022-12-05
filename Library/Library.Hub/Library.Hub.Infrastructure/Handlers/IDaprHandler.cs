using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Handlers
{
    public interface IDaprHandler
    {
        Task PublishMessage<T>(T message);
    }
}