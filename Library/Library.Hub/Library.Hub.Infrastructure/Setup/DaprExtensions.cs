using Library.Hub.Infrastructure.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Library.Hub.Infrastructure.Handlers;
using Library.Hub.Core.Interfaces;

namespace Library.Hub.Infrastructure.Setup
{
    public static class DaprExtensions
    {
        public static IServiceCollection AddDaprService(this IServiceCollection services, string httpUri = "http://localhost:3500", string grpcUri = "http://localhost:50000")
        {
            services.AddDaprClient();

            services.AddSingleton<IDaprHandler>(x => new DaprHandler(httpUri, grpcUri));
            services.AddSingleton<IMessageEventStore<MessageEvent>, MessageEventStore>();

            return services;
        }

        public static IMvcBuilder AddDaprForMvc(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddDapr();

            return mvcBuilder;
        }

        public static IApplicationBuilder UseDaprServices(this IApplicationBuilder app)
        {
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
