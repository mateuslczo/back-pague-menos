using Microsoft.EntityFrameworkCore.Storage;
using OrderDataManagement.Domain.Interfaces;
using OrderDataManagement.Infraestructure.Repositories;
using OrderDataManagement.Infrastructure.Data;
using PagueMenos.Domain.Interfaces;

namespace PagueMenos.Infraestructure.Data
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
				await transaction.DisposeAsync();
			}
		}
	}
}