using DeliVeggie.Gateway.Dto;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace DeliVeggie.Gateway.Controllers
{
	[Route("gateway/[controller]")]
	[ApiController]
	public class ProductDetailResultsController : ControllerBase
	{
		IMemoryCache _memoryCache;

		public ProductDetailResultsController(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		[HttpGet("{transactionId}", Name = "GetProductDetailResults")]
		public ActionResult<GetProductDetailsResponse> GetProducts(string transactionId)
		{
			var response = new GetProductDetailsResponse();
			if (_memoryCache.TryGetValue<Product>(transactionId, out var record))
			{
				response.Record = record;
				response.IsDone = true;
			}

			return Ok(response);
		}
	}
}
