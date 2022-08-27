using Products.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API.Repository
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetProductsAsync();

		Task<Product> GetProductAsync(string id);
	}
}
