using Products.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Repository
{
	public interface IProductRepository
	{
		Task<IEnumerable<DbProduct>> GetProductsAsync();

		Task<DbProduct> GetProductAsync(string id);
	}
}
