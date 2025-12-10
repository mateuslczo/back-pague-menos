using OrderDataManagement.Domain.Interfaces;
using PagMenos.Application.Interfaces.Services;
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

		public Task<PagedResult<T>> GetPagedAsync(int page = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool asNoTracking = true)
		{
			var result = repository.GetPagedAsync(page, pageSize, filter, orderBy, asNoTracking);
			return result;
		}
	}
}
