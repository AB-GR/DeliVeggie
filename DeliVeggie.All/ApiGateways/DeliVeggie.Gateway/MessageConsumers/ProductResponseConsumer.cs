using Messages;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.Gateway.MessageConsumers
{
	public interface IProductResponseConsumer
	{
		void ConsumeProductsResponse(ProductsResponse message);

		void ConsumeProductDetailsResponse(ProductDetailsResponse message);
	}

	public class ProductResponseConsumer : IProductResponseConsumer
	{
		private readonly IMemoryCache _memoryCache;

		public ProductResponseConsumer(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public void ConsumeProductsResponse(ProductsResponse message)
		{
			_memoryCache.Set<List<Product>>(message.TransactionId, message.ProductList);
		}

		public void ConsumeProductDetailsResponse(ProductDetailsResponse message)
		{
			_memoryCache.Set<Product>(message.TransactionId, message.Product);
		}
	}
}
