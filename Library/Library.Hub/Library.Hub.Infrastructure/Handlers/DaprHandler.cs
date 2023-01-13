﻿using Dapr.Client;
using Library.Hub.Core.Interfaces;
using System.Threading.Tasks;

namespace Library.Hub.Infrastructure.Handlers
{
    public class DaprHandler : IDaprHandler
    {
        private readonly DaprClient _daprClient;
        private readonly string PUBSUB_NAME = "library-pub-sub";

        public DaprHandler(string httpUri, string grpcUri) 
        {
            _daprClient = new DaprClientBuilder().UseHttpEndpoint(httpUri).UseGrpcEndpoint(grpcUri).Build();
        }

        public async Task PublishMessage<T>(T message)
        {
            await _daprClient.PublishEventAsync(PUBSUB_NAME, typeof(T).Name, message);
        }
    }
}
