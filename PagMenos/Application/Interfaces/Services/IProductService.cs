using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Domain.Entities;

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

		/// <summary>
		/// Atualiza um produto
		/// </summary>
		/// <param name="product"></param>
		/// <returns>Numero de linhas afetadas</returns>
		Task<int> UpdateProduct(long id, ProductDto product);

		/// <summary>
		/// Insere um produto
		/// </summary>
		/// <param name="product"></param>
		/// <returns>Id do produto</returns>
		Task<long> CreateProduct(ProductDto product);
	}
}
