using OrderDataManagement.Domain.Entities;

namespace PagMenos.Application.Interfaces.Services
{
	public interface IProductService :IGenericService<Product>
	{
		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="description"></param>
		/// <returns>IQueryable<Product></returns>
		Task<List<Product>> GetProductByDescriptionAsync(string description);

		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="product"></param>
		/// <returns>IQueryable<Product></returns>
		Task<List<Product>> GetProductByNameAsync(string product);
	}
}
