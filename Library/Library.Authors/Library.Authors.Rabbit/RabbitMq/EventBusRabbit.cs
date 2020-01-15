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
        private readonly IConfiguration _configuration;
        private readonly IModel _channel;
        private IConnection _connection;

        private const string Exchange = "exchange.author";
        private const string Queue = "queue.author";
        private const string RoutingKey = "*.author";

        public EventBusRabbit(ILogger<EventBusRabbit> logger, IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration.GetSection("RabbitMqConfig");
            _channel = CreateChannel();
        }

        public Task PublishMessage<T>(MessageEvent @event)
        {
            return Task.Run(() =>
            {
                if (_channel.IsClosed)
                    CreateChannel();

                _channel.BasicPublish(
                    Exchange,
                    Queue,
                    null,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
                _logger.LogInformation("Published {0}", typeof(T).Name);

            });
        }

        public Task Subscribe<T,TH>()
            where T : MessageEvent
            where TH : IMessageEventHandler<T>
        {
            return Task.Run(() =>
            {
                if (_channel.IsClosed)
                    CreateChannel();

                _channel.QueueBind(
                    Queue,
                    Exchange,
                    RoutingKey, null);

                _channel.BasicQos(0, 1, false);

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += Consumer_Received<T,TH>;

                _channel.BasicConsume(Queue, false, consumer);
                _logger.LogInformation("Subscribed {0}", typeof(T).Name);

            });

        }

        private IModel CreateChannel()
        {
            if(_connection == null || !_connection.IsOpen)
                _connection = GetConnectionFactory().CreateConnection();

            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(Exchange, "topic");
            channel.QueueDeclare(Queue, true, false, false, null);
            
            _logger.LogInformation("Channel created");

            return channel;
        }

        private ConnectionFactory GetConnectionFactory()
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

        private Task Consumer_Received<T,TH>(object sender, BasicDeliverEventArgs eventArgs) 
            where T : MessageEvent
            where TH : IMessageEventHandler<T>
        {
            return Task.Run(async () =>
            {
                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                _logger.LogInformation("Consumer Received {0}", message);

                var eventMessage = JsonConvert.DeserializeObject<T>(message);

                var type = ((Type[])((TypeInfo)typeof(TH)).ImplementedInterfaces).First();

                var handler = (TH)_serviceProvider.GetService(type);

                await handler.Handle(eventMessage);

                await Task.Yield();
            });
        }
    }
}