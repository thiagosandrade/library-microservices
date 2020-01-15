using Library.Authors.Rabbit.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Authors.Rabbit
{
    public static class RabbitConfig
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBusRabbit>();

            return services;
        }
    }
}
