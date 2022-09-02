using AutoMapper;
using EasyNetQ;
using Messages;
using Products.API.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.MessageConsumers
{
	public interface IProductRequestConsumer
	{
		Task ConsumeProductsRequestAsync(ProductsRequest message);

		Task ConsumeProductDetailsRequestAsync(ProductDetailsRequest message);
	}

	public class ProductRequestConsumer : IProductRequestConsumer
	{
		private readonly IProductManager _productManager;
		private readonly IBus _bus;

		public ProductRequestConsumer(IProductManager productManager, IBus bus)
		{
			_productManager = productManager;
			_bus = bus;
		}

		public async Task ConsumeProductsRequestAsync(ProductsRequest message)
		{
			var products = await _productManager.GetProductsAsync();
			await _bus.PubSub.PublishAsync(new ProductsResponse { TransactionId = message.TransactionId, ProductList = products });
		}

		public async Task ConsumeProductDetailsRequestAsync(ProductDetailsRequest message)
		{
			var product = await _productManager.GetProductByIdAsync(message.ProductId);
			await _bus.PubSub.PublishAsync(new ProductDetailsResponse { TransactionId = message.TransactionId, Product = product });
		}
	}
}
