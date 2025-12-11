using Microsoft.EntityFrameworkCore.Storage;
using PagMenos.Domain.Interfaces;
using PagMenos.Infraestructure.Repositories;
using PagMenos.Infrastructure.Data;

namespace PagMenos.Infraestructure.Data
{
	public class UnitOfWork :IUnitOfWork
	{

		private readonly EFDbContext context;
		private IDbContextTransaction? transaction;

		public UnitOfWork(EFDbContext _context)
		{
			context = _context;
			OrderRepository = new OrderRepository(context);
			ProductRepository = new ProductRepository(context);
			CollaboratorRepository = new CollaboratorRepository(context);
			OrderItemRepository = new OrderItemRepository(context);

		}

		public IOrderRepository OrderRepository { get; }
		public IProductRepository ProductRepository { get; }
		public ICollaboratorRepository CollaboratorRepository { get; }
		public IOrderItemRepository OrderItemRepository { get; }

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}

		public async Task BeginTransactionAsync()
		{

			transaction = await context.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			if (transaction != null)
			{
				await transaction.CommitAsync();
				Dispose();
			}
		}


		public async Task RollbackTransactionAsync()
		{
			if (transaction != null)
			{
				await transaction.RollbackAsync();
				Dispose();
			}

		}

		public void Dispose()
		{
			if (transaction != null)
			{
				transaction.DisposeAsync();
			}
		}
	}
}