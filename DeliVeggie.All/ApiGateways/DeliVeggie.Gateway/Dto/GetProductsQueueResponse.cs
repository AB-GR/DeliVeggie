namespace DeliVeggie.Gateway.Dto
{
	public class GetProductsQueueResponse
	{
		public string TransactionId { get; set; }

		public bool IsQueued { get; set; }
	}
}
