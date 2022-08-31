using EasyNetQ;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DeliVeggie.Gateway.Controllers
{
	[ApiController]
	[Route("gateway/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly ILogger<ProductsController> _logger;
		private readonly IBus _bus;

		public ProductsController(ILogger<ProductsController> logger, IBus bus)
		{
			_logger = logger;
			_bus = bus;
		}

		[HttpGet]
		public async Task<ActionResult> GetProducts()
		{
			await _bus.PubSub.PublishAsync(new ProductsRequest());
			return Ok();
		}

		[HttpGet("{id:length(24)}", Name = "GetProduct")]
		public async Task<ActionResult<Product>> GetProductById(string id)
		{
			await _bus.PubSub.PublishAsync(new ProductDetailsRequest { ProductId = id });
			return Ok();
		}
	}
}
