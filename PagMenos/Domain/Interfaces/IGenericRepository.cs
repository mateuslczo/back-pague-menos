using PagMenos.Infraestructure.DataContexts;
using System.Linq.Expressions;

namespace PagMenos.Domain.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(long id);

		Task AddAsync(T t);

		void Remove(T t);

		void Update(T t);

		Task<int> CommitChangesAsync();

		Task<PagedResult<T>> PaginatedListAsync(
		   int page,
		   int pageSize,
		   Expression<Func<T, bool>>? filter = null,
		   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		   string[]? includes = null,
		   bool asNoTracking = true
	   );
	}
}