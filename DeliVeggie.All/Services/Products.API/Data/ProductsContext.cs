using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Products.API.Entities;

namespace Products.API.Data
{
	public class ProductsContext : IProductsContext
	{
        public ProductsContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductsCollectionName"));
            PriceReductions = database.GetCollection<PriceReduction>(configuration.GetValue<string>("DatabaseSettings:PriceReductionsCollectionName"));
            ProductsContextSeed.SeedData(this);
        }

        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<PriceReduction> PriceReductions { get; }
    }
}
