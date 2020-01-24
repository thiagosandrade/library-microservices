using Library.Hub.Rabbit.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Library.Hub.Rabbit.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Hub.Rabbit
{
    public static class RabbitConfig
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBusRabbit>();
            services.AddSingleton<IMessageEventStore, MessageEventStore>();

            return services;
        }
    }
}