using DeliVeggie.Gateway.Dto;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace DeliVeggie.Gateway.Controllers
{
	[Route("gateway/[controller]")]
	[ApiController]
	public class ProductResultsController : ControllerBase
	{
		IMemoryCache _memoryCache;

		public ProductResultsController(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		[HttpGet("{transactionId}", Name = "GetProductResults")]
		public ActionResult<GetProductsResponse> GetProducts(string transactionId)
		{
			var response = new GetProductsResponse();
			if (_memoryCache.TryGetValue<List<Product>>(transactionId, out var records))
			{
				response.Records = records;
				response.IsDone = true;
			}

			return Ok(response);
		}
	}
}
