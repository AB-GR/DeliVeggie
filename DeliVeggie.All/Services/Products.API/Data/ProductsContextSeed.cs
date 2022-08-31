using MongoDB.Driver;
using Products.API.Entities;
using System.Collections.Generic;

namespace Products.API.Data
{
	public class ProductsContextSeed
	{
        public static  void SeedData(IProductsContext context)
        {
            bool productExists = context.Products.Find(p => true).Any();
            if (!productExists)
            {
                context.Products.InsertMany(GetPreconfiguredProducts());
            }

            bool priceReductionExists = context.PriceReductions.Find(p => true).Any();
            if (!priceReductionExists)
            {
                context.PriceReductions.InsertMany(GetPreconfiguredPriceReductions());
            }
        }

        private static List<DbProduct> GetPreconfiguredProducts()
        {
            return new List<DbProduct>()
            {
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    //EntryDate = DateTime.UtcNow,
                    Price = 1000
                },
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    //EntryDate = DateTime.UtcNow,
                    Price = 500
                },
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    //EntryDate = DateTime.UtcNow,
                    Price = 400
                },
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    //EntryDate = DateTime.UtcNow,
                    Price = 300
                },
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    //EntryDate = DateTime.UtcNow,
                    Price = 450
                },
                new DbProduct()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    //EntryDate = DateTime.UtcNow,
                    Price = 339
                }
            };
        }

        private static List<DbPriceReduction> GetPreconfiguredPriceReductions()
        {
            return new List<DbPriceReduction>()
            {
                new DbPriceReduction()
                {
                    DayOfWeek = 1,
                    Reduction = 0
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 2,
                    Reduction = 0
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 3,
                    Reduction = 0
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 4,
                    Reduction = 0
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 5,
                    Reduction = 0
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 6,
                    Reduction = 0.2m
                },
                new DbPriceReduction()
                {
                    DayOfWeek = 7,
                    Reduction = 0.5m
                }
            };
        }
    }
}
