using DeliVeggie.Gateway.Dto;
using EasyNetQ;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace DeliVeggie.Gateway.Controllers
{
	[ApiController]
	[Route("gateway/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IBus _bus;
		IMemoryCache _memoryCache;

		public ProductsController(IBus bus, IMemoryCache memoryCache)
		{
			_bus = bus;
			_memoryCache = memoryCache;
		}

		[HttpGet]
		public async Task<ActionResult<GetProductsQueueResponse>> GetProducts()
		{
			var productsRequest = new ProductsRequest { TransactionId = Guid.NewGuid().ToString() };
			await _bus.PubSub.PublishAsync(productsRequest);
			return Ok(new GetProductsQueueResponse { IsQueued = true, TransactionId = productsRequest.TransactionId });
		}


		[HttpGet("{id:length(24)}", Name = "GetProduct")]
		public async Task<ActionResult<GetProductDetailsQueueResponse>> GetProductById(string id)
		{
			var productDetailsRequest = new ProductDetailsRequest { TransactionId = Guid.NewGuid().ToString(), ProductId = id };
			await _bus.PubSub.PublishAsync(productDetailsRequest);
			return Ok(new GetProductDetailsQueueResponse { IsQueued = true, TransactionId = productDetailsRequest.TransactionId });
		}
	}
}
