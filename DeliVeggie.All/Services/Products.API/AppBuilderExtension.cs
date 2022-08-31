using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.API.MessageConsumers;
using Messages;

namespace Products.API
{
	public static class AppBuilderExtension
	{
        public static IApplicationBuilder UseEasyNetQSubscribe(this IApplicationBuilder appBuilder, string subscriptionIdPrefix)
        {
            var services = appBuilder.ApplicationServices.CreateScope().ServiceProvider;

            var lifeTime = services.GetService<IHostApplicationLifetime>();
            var bus = services.GetService<IBus>();
            var productRequestConsumer = services.GetService<IProductRequestConsumer>();

            lifeTime.ApplicationStarted.Register(async () =>
            {
                await bus.PubSub.SubscribeAsync<ProductsRequest>(subscriptionIdPrefix, productRequestConsumer.ConsumeProductsRequestAsync);
                await bus.PubSub.SubscribeAsync<ProductDetailsRequest>(subscriptionIdPrefix, productRequestConsumer.ConsumeProductDetailsRequestAsync);
            });

            lifeTime.ApplicationStopped.Register(() => bus.Dispose());

            return appBuilder;
        }
    }
}
