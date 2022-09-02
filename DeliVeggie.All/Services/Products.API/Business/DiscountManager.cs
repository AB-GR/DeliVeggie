using Messages;
using Products.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API.Business
{
	public interface IDiscountManager
	{
		Task<List<Product>> ApplyDiscounts(List<Product> products);

		Task<Product> ApplyDiscount(Product product);
	}

	public class DiscountManager : IDiscountManager
	{
		private readonly IDiscountRepository _discountRepository;

		public DiscountManager(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		public async Task<Product> ApplyDiscount(Product product)
		{
			var priceReductions = await _discountRepository.GetPriceReductionsAsync();
			var priceReduction = priceReductions.FirstOrDefault(x => x.DayOfWeek == (int)DateTime.UtcNow.DayOfWeek);

			if (priceReduction != null)
			{
				product.Price -= product.Price * priceReduction.Reduction;
			}

			return product;
		}

		public async Task<List<Product>> ApplyDiscounts(List<Product> products)
		{
			var priceReductions = await _discountRepository.GetPriceReductionsAsync();
			var priceReduction = priceReductions.FirstOrDefault(x => x.DayOfWeek == (int)DateTime.UtcNow.DayOfWeek);

			if (priceReduction != null)
			{
				foreach (var product in products)
				{
					product.Price -= product.Price * priceReduction.Reduction;
				}
			}

			return products;
		}
	}
}
