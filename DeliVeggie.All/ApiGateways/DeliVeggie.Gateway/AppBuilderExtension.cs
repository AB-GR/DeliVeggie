using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Messages;
using DeliVeggie.Gateway.MessageConsumers;

namespace DeliVeggie.Gateway
{
	public static class AppBuilderExtension
	{
        public static IApplicationBuilder UseEasyNetQSubscribe(this IApplicationBuilder appBuilder, string subscriptionIdPrefix)
        {
            var services = appBuilder.ApplicationServices.CreateScope().ServiceProvider;

            var lifeTime = services.GetService<IHostApplicationLifetime>();
            var bus = services.GetService<IBus>();
            var productResponseConsumer = services.GetService<IProductResponseConsumer>();

            lifeTime.ApplicationStarted.Register(async () =>
            {
                await bus.PubSub.SubscribeAsync<ProductsResponse>(subscriptionIdPrefix, productResponseConsumer.ConsumeProductsResponse);
                await bus.PubSub.SubscribeAsync<ProductDetailsResponse>(subscriptionIdPrefix, productResponseConsumer.ConsumeProductDetailsResponse);
            });

            lifeTime.ApplicationStopped.Register(() => bus.Dispose());

            return appBuilder;
        }
    }
}
