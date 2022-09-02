using AutoMapper;
using Messages;
using Products.API.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Business
{
	public interface IProductManager
	{
		Task<List<Product>> GetProductsAsync();

		Task<Product> GetProductByIdAsync(string id);
	}

	public class ProductManager : IProductManager
	{
		private readonly IDiscountManager _discountManager;
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;

		public ProductManager(IDiscountManager discountManager, IProductRepository repository, IMapper mapper)
		{
			_discountManager = discountManager;
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_mapper = mapper;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			var dbProducts = await _repository.GetProductsAsync();
			var products = _mapper.Map<List<Product>>(dbProducts);
			products = await _discountManager.ApplyDiscountsAsync(products);

			return products;
		}

		public async Task<Product> GetProductByIdAsync(string id)
		{
			var dbProduct = await _repository.GetProductByIdAsync(id);
			if (dbProduct == null)
			{
				throw new Exception($"Product with id: {id}, not found.");
			}

			var product = _mapper.Map<Product>(dbProduct);
			product = await _discountManager.ApplyDiscountAsync(product);

			return product;
		}
	}
}
