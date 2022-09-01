using System.Collections.Generic;

namespace Messages
{
	public class ProductsResponse
	{
		public string TransactionId { get; set; }
		public List<Product> ProductList { get; set; }
	}
}
