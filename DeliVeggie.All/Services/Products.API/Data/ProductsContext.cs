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

            Products = database.GetCollection<DbProduct>(configuration.GetValue<string>("DatabaseSettings:ProductsCollectionName"));
            PriceReductions = database.GetCollection<DbPriceReduction>(configuration.GetValue<string>("DatabaseSettings:PriceReductionsCollectionName"));
            ProductsContextSeed.SeedData(this);
        }

        public IMongoCollection<DbProduct> Products { get; }

        public IMongoCollection<DbPriceReduction> PriceReductions { get; }
    }
}
