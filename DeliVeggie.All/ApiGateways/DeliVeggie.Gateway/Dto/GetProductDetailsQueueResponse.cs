namespace DeliVeggie.Gateway.Dto
{
	public class GetProductDetailsQueueResponse
	{
		public string TransactionId { get; set; }

		public bool IsQueued { get; set; }
	}
}
