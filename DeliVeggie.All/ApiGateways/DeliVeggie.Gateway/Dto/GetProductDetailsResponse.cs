using Messages;

namespace DeliVeggie.Gateway.Dto
{
	public class GetProductDetailsResponse
	{
		public bool IsDone { get; set; }

		public Product Record { get; set; }
	}
}
