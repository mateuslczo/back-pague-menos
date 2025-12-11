using PagMenos.Domain.Entities;

namespace PagMenos.Domain.Interfaces
{
	public interface IProductRepository :IGenericRepository<Product>
	{

		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="description"></param>
		/// <returns>IQueryable<Product></returns>
		IQueryable<Product> GetProductByDescriptionAsync(string description);

		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="product"></param>
		/// <returns>IQueryable<Product></returns>
		IQueryable<Product> GetProductByNameAsync(string product);

	}
}
