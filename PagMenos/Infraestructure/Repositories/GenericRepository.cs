
using Microsoft.EntityFrameworkCore;
using OrderDataManagement.Infrastructure.Data;
using PagMenos.Infraestructure.DataContexts;
using System.Linq.Expressions;

namespace OrderDataManagement.Domain.Interfaces
{
	public class GenericRepository<T> :IGenericRepository<T> where T : class

	{
		private readonly EFDbContext context;

		public GenericRepository(EFDbContext _context)
		{
			context = _context;
		}

		public async Task AddAsync(T t)
		{

			await context.Set<T>().AddAsync(t);

		}

		public async Task<T?> GetByIdAsync(long id)
		{
			var result = await context.Set<T>().FindAsync(id);
			return result;
		}

		public void Update(T t)
		{

			context.Set<T>().Entry(t).State = EntityState.Modified;

		}

		public void Remove(T t)
		{
			context.Set<T>().Remove(t).State = EntityState.Deleted;
		}

		public async Task<PagedResult<T>> GetPagedAsync(int page=1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool asNoTracking = true)
		{
			IQueryable<T> query = context.Set<T>();

			if (asNoTracking)
				query = query.AsNoTracking();

			if (filter != null)
				query = query.Where(filter);

			int totalItems = await query.CountAsync();

			if (orderBy != null)
				query = orderBy(query);

			var items = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResult<T>
			{
				Items = items,
				Page = page,
				PageSize = pageSize,
				TotalItems = totalItems
			};
		}
	}
}
