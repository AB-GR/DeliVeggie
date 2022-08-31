using Messages;
using System.Threading.Tasks;

namespace DeliVeggie.Gateway.MessageConsumers
{
	public interface IProductResponseConsumer
	{
		Task ConsumeProductsResponseAsync(ProductsResponse message);

		Task ConsumeProductDetailsResponseAsync(ProductDetailsResponse message);
	}

	public class ProductResponseConsumer : IProductResponseConsumer
	{
		public Task ConsumeProductsResponseAsync(ProductsResponse message)
		{
			//throw new NotImplementedException();
			return Task.CompletedTask;
		}

		public Task ConsumeProductDetailsResponseAsync(ProductDetailsResponse message)
		{
			//throw new NotImplementedException();
			return Task.CompletedTask;
		}
	}
}
