using Messages;
using System.Collections.Generic;

namespace DeliVeggie.Gateway.Dto
{
	public class GetProductsResponse
	{
		public bool IsDone { get; set; }

		public List<Product> Records { get; set; }
	}
}
