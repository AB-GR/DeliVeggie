using AutoMapper;
using Moq;
using NUnit.Framework;
using Products.API;
using Products.API.Business;
using Products.API.Entities;
using Products.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Tests
{
	public class ProductManagerTests
	{
		private readonly Mock<IProductRepository> mockRepository;
		private List<DbProduct> products;
        private IProductManager productManager;

		public ProductManagerTests()
		{
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            mockRepository = new Mock<IProductRepository>();
            productManager = new ProductManager(mockRepository.Object, mapper);

        }

		[SetUp]
		public void Setup()
		{
			Init();
			mockRepository.Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult(products));
			mockRepository.Setup(x => x.GetProductByIdAsync(It.IsAny<string>())).Returns<string>((id) => Task.FromResult(products.Where(x => x.Id == id).FirstOrDefault()));
		}

		[Test]
		public async Task GetProductsAsync()
		{
			var result = await productManager.GetProductsAsync();
            Assert.That(result.Count, Is.EqualTo(6));
        }

		[Test]
		public async Task GetProductByIdAsync()
		{
            var result = await productManager.GetProductByIdAsync("602d2149e773f2a3990b47f5");
            Assert.That(result.Id, Is.EqualTo("602d2149e773f2a3990b47f5"));
        }

        [Test]
        public void GetProductByIdAsync_Throws()
        {
            var exception = Assert.ThrowsAsync<Exception>(async () => await productManager.GetProductByIdAsync("dummy"));
            Assert.That(exception.Message, Is.EqualTo("Product with id: dummy, not found."));
        }

        private void Init()
		{
			products = new List<DbProduct>()
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
	}
}