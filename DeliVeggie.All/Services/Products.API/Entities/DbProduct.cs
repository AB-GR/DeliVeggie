using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Products.API.Entities
{
	public class DbProduct
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement]
		public string Name { get; set; }

		[BsonElement]
		public DateTime EntryDate { get; set; }

		[BsonElement]
		public decimal Price { get; set; }
	}
}
