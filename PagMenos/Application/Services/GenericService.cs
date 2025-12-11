using PagMenos.Application.Interfaces.Services;
using PagMenos.Domain.Interfaces;
using PagMenos.Infraestructure.DataContexts;
using System.Linq.Expressions;

namespace PagMenos.Application.Services
{
	public class GenericService<T> :IGenericService<T> where T : class
	{

		private readonly IGenericRepository<T> repository;

		public GenericService(IGenericRepository<T> _repository)
		{
			repository = _repository;
		}


		public async Task<int> CommitAsync()
		{
			var affectedRecno = await repository.CommitChangesAsync();
			return affectedRecno;
		}

		public async Task AddAsync(T t)
		{
			await repository.AddAsync(t);
		}

		public async Task<T?> GetByIdAsync(long id)
		{
			var result = await repository.GetByIdAsync(id);
			return result;
		}

		public void Update(T t)
		{

			repository.Update(t);

		}

		public void Remove(T t)
		{
			repository.Remove(t);
		}

		public Task<PagedResult<T>> PaginatedListAsync(int page = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string[]? includes = null, bool asNoTracking = true)
		{
			var result = repository.PaginatedListAsync(page, pageSize, filter, orderBy, includes, asNoTracking);
			return result;
		}

	}
}
