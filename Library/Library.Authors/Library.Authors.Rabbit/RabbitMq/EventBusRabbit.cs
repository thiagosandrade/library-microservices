// ---------------------------------------------------------------------------------------
// <copyright file="EventBusRabbit.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Library.Authors.Rabbit.RabbitMq
{
    public class EventBusRabbit : IEventBus
    {
        private readonly ILogger<EventBusRabbit> _logger;
        private readonly IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;
        private IModel _channel;
        private IConnection _connection;

        private readonly string Exchange;
        private readonly string Queue;
        private readonly string RoutingKey;

        public EventBusRabbit(ILogger<EventBusRabbit> logger, IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration.GetSection("RabbitMqConfig");

            Exchange = _configuration.GetValue<string>("Exchange").ToString();
            Queue = _configuration.GetValue<string>("Queue").ToString();
            RoutingKey = _configuration.GetValue<string>("RoutingKey").ToString();
            CreateChannel();
        }

        public Task PublishMessage<T>(MessageEvent @event)
        {
            return Task.Run(() =>
            {
                if (_channel == null || _channel.IsClosed)
                    CreateChannel();

                _channel.BasicPublish(
                    Exchange,
                    RoutingKey,
                    null,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
                _logger.LogInformation("Published {0}", typeof(T).Name);

            });
        }

        public Task Subscribe<T, TH>()
            where T : MessageEvent
            where TH : IMessageEventHandler<T>
        {
            _logger.LogInformation("Subscribing..{0}-{1}", typeof(T).Name, typeof(TH).Name);

            return Task.Run(() =>
            {
                if (_channel == null || _channel.IsClosed)
                    CreateChannel();

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += Consumer_Received<T, TH>;

                _channel.BasicConsume(Queue, false, consumer);

                Activator.CreateInstance<IServiceCollection>()
                    .AddTransient(typeof(IMessageEventHandler<T>), typeof(TH));

                _logger.LogInformation("Subscribed {0}", typeof(T).Name);
            });
        }

        private void CreateChannel()
        {
            if (_connection == null || !_connection.IsOpen)
                _connection = GetConnectionFactory().CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Exchange, "topic");
            _channel.QueueDeclare(Queue, false, false, false, null);
            _channel.BasicQos(0, 1, false);

            _channel.QueueBind(
                    Queue,
                    Exchange,
                    RoutingKey, null);

            _logger.LogInformation("Channel created");
        }

        private static ConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("HostName"),
                UserName = _configuration.GetValue<string>("UserName"),
                Password = _configuration.GetValue<string>("Password"),
                Port = _configuration.GetValue<int>("Port"),
                DispatchConsumersAsync = _configuration.GetValue<bool>("DispatchConsumersAsync")
            };
            return connectionFactory;
        }

        private Task Consumer_Received<T, TH>(object sender, BasicDeliverEventArgs eventArgs)
            where T : MessageEvent
            where TH : IMessageEventHandler<T>
        {
            return Task.Run(async () =>
            {
                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                _logger.LogInformation("Consumer Received {0}", message);

                var eventMessage = JsonConvert.DeserializeObject<T>(message);

                var type = ((Type[])((TypeInfo)typeof(TH)).ImplementedInterfaces)
                            .Where(x => x.Name.Contains(nameof(TH))).FirstOrDefault();

                var handler = (TH)_serviceProvider.GetService(type);

                await handler.Handle(eventMessage);

                await Task.Yield();
            });
        }
    }
}