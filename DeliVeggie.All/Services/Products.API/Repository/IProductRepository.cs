using Products.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Repository
{
	public interface IProductRepository
	{
		Task<List<DbProduct>> GetProductsAsync();

		Task<DbProduct> GetProductByIdAsync(string id);
	}
}
