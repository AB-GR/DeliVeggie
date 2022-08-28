using MongoDB.Driver;
using Products.API.Entities;
using System;
using System.Collections.Generic;

namespace Products.API.Data
{
	public class ProductsContextSeed
	{
        public static void SeedData(IProductsContext context)
        {
            bool productExists = context.Products.Find(p => true).Any();
            if (!productExists)
            {
                context.Products.InsertManyAsync(GetPreconfiguredProducts());
            }

            bool priceReductionExists = context.PriceReductions.Find(p => true).Any();
            if (!priceReductionExists)
            {
                context.PriceReductions.InsertManyAsync(GetPreconfiguredPriceReductions());
            }
        }

        private static List<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    EntryDate = DateTime.UtcNow,
                    Price = 1000
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    EntryDate = DateTime.UtcNow,
                    Price = 500
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    EntryDate = DateTime.UtcNow,
                    Price = 400
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    EntryDate = DateTime.UtcNow,
                    Price = 300
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    EntryDate = DateTime.UtcNow,
                    Price = 450
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    EntryDate = DateTime.UtcNow,
                    Price = 339
                }
            };
        }

        private static List<PriceReduction> GetPreconfiguredPriceReductions()
        {
            return new List<PriceReduction>()
            {
                new PriceReduction()
                {
                    DayOfWeek = 1,
                    Reduction = 0
                },
                new PriceReduction()
                {
                    DayOfWeek = 2,
                    Reduction = 0
                },
                new PriceReduction()
                {
                    DayOfWeek = 3,
                    Reduction = 0
                },
                new PriceReduction()
                {
                    DayOfWeek = 4,
                    Reduction = 0
                },
                new PriceReduction()
                {
                    DayOfWeek = 5,
                    Reduction = 0
                },
                new PriceReduction()
                {
                    DayOfWeek = 6,
                    Reduction = 0.2m
                },
                new PriceReduction()
                {
                    DayOfWeek = 7,
                    Reduction = 0.5m
                }
            };
        }
    }
}
