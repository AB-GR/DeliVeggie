using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.API.Entities
{
	public class DbPriceReduction
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement]
		public int DayOfWeek { get; set; }

		[BsonElement]
		public decimal Reduction { get; set; }
	}
}
