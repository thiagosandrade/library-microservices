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
using Library.Hub.Rabbit.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Library.Hub.Rabbit.RabbitMq
{
    public class EventBusRabbit : IEventBus
    {
        private readonly ILogger<EventBusRabbit> _logger;
        private readonly IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;
        private IModel _channel;
        private IConnection _connection;

        private readonly string _exchange;
        private readonly string _queue;
        private readonly string _routingKey;

        public EventBusRabbit(ILogger<EventBusRabbit> logger, IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration.GetSection("RabbitMqConfig");
            
            _exchange = _configuration.GetValue<string>("Exchange");
            _queue = _configuration.GetValue<string>("Queue");
            _routingKey = _configuration.GetValue<string>("RoutingKey");
            CreateChannel();
        }

        public Task PublishMessage<T>(IMessageEvent @event)
        {
            return Task.Run(() =>
            {
                if (_channel == null || _channel.IsClosed)
                    CreateChannel();

                var encapsulatedEvent = new MessageEvent(@event);

                _channel.BasicPublish(
                    _exchange,
                    _routingKey,
                    null,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(encapsulatedEvent)));
                _logger.LogInformation("Published {0}", typeof(T).Name);

            });
        }

        public Task Subscribe<T,TH>()
            where T : IMessageEvent, new()
            where TH : IMessageEventHandler<T>
        {
            _logger.LogInformation("Subscribing..{0}-{1}", typeof(T).Name, typeof(TH).Name);

            return Task.Run(() =>
            {
                if (_channel == null || _channel.IsClosed)
                   CreateChannel();

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += Consumer_Received<T,TH>;

                _channel.BasicConsume(_queue, false, consumer);

                Activator.CreateInstance<ServiceCollection>()
                    .AddTransient(typeof(IMessageEventHandler<T>), typeof(TH));

                _logger.LogInformation("Subscribed {0}", typeof(T).Name);
            });
        }

        private void CreateChannel()
        {
            if(_connection == null || !_connection.IsOpen)
                _connection = GetConnectionFactory().CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchange, "topic");
            _channel.QueueDeclare(_queue, false, false, false, null);
            _channel.BasicQos(0, 1, false);

            _channel.QueueBind(
                    _queue,
                    _exchange,
                    _routingKey, null);

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

        private Task Consumer_Received<T,TH>(object sender, BasicDeliverEventArgs eventArgs) 
            where T : IMessageEvent, new()
            where TH : IMessageEventHandler<T>
        {
            return Task.Run(async () =>
            {
                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                _logger.LogInformation("Consumer Received {0}", message);

                var encapsulatedMessage = JsonConvert.DeserializeObject<MessageEvent>(message);

                if(typeof(TH).GetInterfaces().Any(x => x.IsGenericType &&
                                                       x.GetGenericTypeDefinition() == typeof(IMessageEventHandler<>)))
                {
                    var eventMessage = new T
                    {
                        Message = encapsulatedMessage.Message
                    };

                    // determine type here
                    var type = typeof(TH);

                    var logger = _serviceProvider.GetRequiredService<ILogger<TH>>();
                    
                    // create an object of the type
                    var handler = (TH)Activator.CreateInstance(type, logger);

                    await handler.Handle(eventMessage);
                }
                else
                {
                    var handler = (IMessageEventHandler<MessageEvent>)_serviceProvider.GetService(typeof(IMessageEventHandler<MessageEvent>));
                    await handler.Handle(encapsulatedMessage);
                }


                await Task.Yield();
            });
        }
    }
}