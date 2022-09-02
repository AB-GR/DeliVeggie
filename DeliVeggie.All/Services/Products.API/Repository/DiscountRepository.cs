using MongoDB.Driver;
using Products.API.Data;
using Products.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Repository
{
	public interface IDiscountRepository
	{
		Task<List<DbPriceReduction>> GetPriceReductionsAsync();
	}

	public class DiscountRepository : IDiscountRepository
	{
		private readonly IProductsContext _context;

		public DiscountRepository(IProductsContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}


		public async Task<List<DbPriceReduction>> GetPriceReductionsAsync()
		{
			return await _context
						 .PriceReductions
						 .Find(p => true)
						 .ToListAsync();
		}
	}
}
