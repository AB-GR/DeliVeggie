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
        private readonly IDiscountManager _discountManager;
		private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IDiscountRepository> _mockDiscountRepository;
        private List<DbProduct> _products;
        private List<DbPriceReduction> _priceReductions;
        private IProductManager _productManager;

		public ProductManagerTests()
		{
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            _mockDiscountRepository = new Mock<IDiscountRepository>();
            _discountManager = new DiscountManager(_mockDiscountRepository.Object);
            _mockProductRepository = new Mock<IProductRepository>();
            _productManager = new ProductManager(_discountManager, _mockProductRepository.Object, mapper);

        }

		[SetUp]
		public void Setup()
		{
			Init();
            _mockDiscountRepository.Setup(x => x.GetPriceReductionsAsync()).Returns(() => Task.FromResult(_priceReductions));
            _mockProductRepository.Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult(_products));
			_mockProductRepository.Setup(x => x.GetProductByIdAsync(It.IsAny<string>())).Returns<string>((id) => Task.FromResult(_products.Where(x => x.Id == id).FirstOrDefault()));
		}

		[Test]
		public async Task GetProductsAsync()
		{
			var result = await _productManager.GetProductsAsync();
            Assert.That(result.Count, Is.EqualTo(6));
        }

		[Test]
		public async Task GetProductByIdAsync()
		{
            var result = await _productManager.GetProductByIdAsync("602d2149e773f2a3990b47f5");
            Assert.That(result.Id, Is.EqualTo("602d2149e773f2a3990b47f5"));
        }

        [Test]
        public void GetProductByIdAsync_Throws()
        {
            var exception = Assert.ThrowsAsync<Exception>(async () => await _productManager.GetProductByIdAsync("dummy"));
            Assert.That(exception.Message, Is.EqualTo("Product with id: dummy, not found."));
        }

        [Test]
        public async Task GetProductByIdAsync_PriceReduction()
        {
            _mockDiscountRepository.Setup(x => x.GetPriceReductionsAsync()).Returns(() => Task.FromResult(new List<DbPriceReduction> { new DbPriceReduction { DayOfWeek = (int)DateTime.UtcNow.DayOfWeek, Reduction = 0.5m } }));
            var result = await _productManager.GetProductByIdAsync("602d2149e773f2a3990b47f5");
            Assert.That(result.Id, Is.EqualTo("602d2149e773f2a3990b47f5"));
            Assert.That(result.Price, Is.EqualTo(500));
        }

        private void Init()
		{
			_products = new List<DbProduct>()
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

            _priceReductions = new List<DbPriceReduction>()
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