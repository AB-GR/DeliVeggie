using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Products.API.Entities
{
	public class Product
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string Name { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime EntryDate { get; set; }

		public decimal Price { get; set; }
	}
}
