using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.API.Entities
{
	public class Product
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string Name { get; set; }

		public BsonDateTime EntryDate { get; set; }

		public decimal Price { get; set; }
	}
}
