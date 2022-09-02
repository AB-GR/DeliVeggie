using MongoDB.Driver;
using Products.API.Data;
using Products.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Repository
{
	public interface IProductRepository
	{
		Task<List<DbProduct>> GetProductsAsync();

		Task<DbProduct> GetProductByIdAsync(string id);
	}

	public class ProductRepository : IProductRepository
	{
		private readonly IProductsContext _context;

		public ProductRepository(IProductsContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<DbProduct> GetProductByIdAsync(string id)
		{
			return await _context
						 .Products
						 .Find(p => p.Id == id)
						 .FirstOrDefaultAsync();
		}

		public async Task<List<DbProduct>> GetProductsAsync()
		{
			return await _context
							.Products
							.Find(p => true)
							.ToListAsync();
		}
	}
}
