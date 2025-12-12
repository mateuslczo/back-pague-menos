using PagMenos.Infraestructure.DataContexts;
using System.Linq.Expressions;

namespace PagMenos.Application.Interfaces.Services
{
	public interface IGenericService<T> where T : class
	{

		Task<T?> GetByIdAsync(long id);

		Task AddAsync(T t);

		void Remove(T t);

		void Update(T t);

		Task<int> CommitAsync();

		Task<PagedResult<T>> PaginatedListAsync(
		   int page = 1,
		   int pageSize = 10,
		   Expression<Func<T, bool>>? filter = null,
		   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		   string[]? includes = null,
		   bool asNoTracking = true
	   );

	}
}
