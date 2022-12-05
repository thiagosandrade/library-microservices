using Dapr.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Handlers
{
    public class DaprHandler : IDaprHandler
    {
        private readonly DaprClient _daprClient;
        private readonly string PUBSUB_NAME = "library-pub-sub";
        private readonly ILogger _logger;


        public DaprHandler(ILogger<DaprClient> logger) 
        {
            _daprClient = new DaprClientBuilder().Build();
            _logger = logger;
        }

        public async Task PublishMessage<T>(T message)
        {
            _logger.LogInformation("Publish Message received - {0}", message);
            _logger.LogInformation("Publish Message type - {0}", typeof(T).Name);

            await _daprClient.PublishEventAsync(PUBSUB_NAME, typeof(T).Name, message);
        }
    }
}
