using PagMenos.Domain.Entities;
using PagMenos.Domain.Interfaces;
using PagMenos.Infrastructure.Data;

namespace PagMenos.Infraestructure.Repositories
{

	public class ProductRepository :GenericRepository<Product>, IProductRepository
	{
		private readonly EFDbContext context;

		public ProductRepository(EFDbContext _context) : base(_context)
		{
			context = _context;
		}


		public IQueryable<Product> GetProductByDescriptionAsync(string description)
		{
			var result = context.Products.Where(p => p.Description.StartsWith(description));
			return result;
		}

		public IQueryable<Product> GetProductByNameAsync(string product)
		{
			var result = context.Products.Where(p => p.ProductName.Contains(product));
			return result;
		}

	}
}
