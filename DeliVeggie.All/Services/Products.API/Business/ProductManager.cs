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
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;

		public ProductManager(IProductRepository repository, IMapper mapper)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_mapper = mapper;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			var dbProducts = await _repository.GetProductsAsync();
			return _mapper.Map<List<Product>>(dbProducts);
		}

		public async Task<Product> GetProductByIdAsync(string id)
		{
			var dbProduct = await _repository.GetProductByIdAsync(id);
			if (dbProduct == null)
			{
				throw new Exception($"Product with id: {id}, not found.");
			}

			return _mapper.Map<Product>(dbProduct);
		}
	}
}
