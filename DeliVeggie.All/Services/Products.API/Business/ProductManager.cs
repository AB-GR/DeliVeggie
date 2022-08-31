using Products.API.Entities;
using Products.API.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Business
{
	public interface IProductManager
	{
		Task<IEnumerable<DbProduct>> GetProductsAsync();

		Task<DbProduct> GetProductByIdAsync(string id);
	}

	public class ProductManager : IProductManager
	{
		private readonly IProductRepository _repository;

		public ProductManager(IProductRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task<IEnumerable<DbProduct>> GetProductsAsync()
		{
			return await _repository.GetProductsAsync();
		}

		public async Task<DbProduct> GetProductByIdAsync(string id)
		{
			var product = await _repository.GetProductAsync(id);
			if (product == null)
			{
				throw new Exception($"Product with id: {id}, not found.");
			}

			return product;
		}
	}
}
