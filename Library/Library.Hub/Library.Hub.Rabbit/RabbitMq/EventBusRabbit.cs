using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Hub.Rabbit.Events;
using Library.Hub.Rabbit.Events.Interfaces;
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

        public EventBusRabbit(ILogger<EventBusRabbit> logger, IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration.GetSection("RabbitMqConfig");
            
            _exchange = _configuration.GetValue<string>("Exchange");
            _queue = _configuration.GetValue<string>("Queue");
            //CreateChannel();
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
                    $"{typeof(T).Name.ToLower()}",
                    null,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(encapsulatedEvent, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        MaxDepth = 3
                    })));
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

                CreateQueue<T, TH>();

                Activator.CreateInstance<ServiceCollection>()
                    .AddTransient(typeof(IMessageEventHandler<T>), typeof(TH));

                _logger.LogInformation("Subscribed {0}", typeof(T).Name);
            });
        }

        private void CreateChannel()
        {
            if(_connection is null || !_connection.IsOpen)
                _connection = GetConnectionFactory().CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchange, "topic");
            _channel.BasicQos(0, 1, false);

            _logger.LogInformation("Channel created");
        }

        private void CreateQueue<T, TH>()
            where T : IMessageEvent, new()
            where TH : IMessageEventHandler<T>
        {
            var key = typeof(T).Name.Equals("MessageEvent") ? "*" : typeof(T).Name.ToLower();
            
            var typeQueue = $"{_queue}-{typeof(T).Name}";
            
            _channel.QueueDeclare(typeQueue, false, false, false, null);
            _channel.QueueBind(
                typeQueue,
                _exchange,
                key, 
                null);


            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received<T, TH>;

            _channel.BasicConsume(typeQueue, false, consumer);

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


                if (ExistsMessageHandlerImplementation<T, TH>())
                {
                    var eventMessage = new T
                    {
                        Message = encapsulatedMessage.Message
                    };

                    var logger = _serviceProvider.GetRequiredService<ILogger<TH>>();
                    
                    // create an object of the type
                    var handler = (TH)Activator.CreateInstance(typeof(TH), logger);

                    await handler.Handle(eventMessage);
                }
                else
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<TH>>();
                    var messageEventStore = _serviceProvider.GetRequiredService<IMessageEventStore>();

                    var handler = (MessageEventHandler)Activator.CreateInstance(typeof(MessageEventHandler), logger, messageEventStore);

                    await handler.Handle(encapsulatedMessage);
                }


                await Task.Yield();
            });
        }

        private static bool ExistsMessageHandlerImplementation<T, TH>() 
            where T : IMessageEvent, new() 
            where TH : IMessageEventHandler<T>
        {
            return typeof(TH).GetInterfaces().Any(x => x.IsGenericType &&
                                                       x.GetGenericTypeDefinition() == typeof(IMessageEventHandler<>))
                && !typeof(TH).Name.Equals("MessageEventHandler");
        }
    }
}