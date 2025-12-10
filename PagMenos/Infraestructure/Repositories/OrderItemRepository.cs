using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using OrderDataManagement.Infrastructure.Data;

namespace OrderDataManagement.Infraestructure.Repositories
{

	public class OrderItemRepository :GenericRepository<OrderItem>, IOrderItemRepository
	{
		private readonly EFDbContext context;

		public OrderItemRepository(EFDbContext _context) : base(_context)
		{
			context = _context;
		}


		public IQueryable<OrderItem> GetOrderItemByProduct(string productName)
		{
			var result = context.OrderItems.Where(p => p.Product.ProductName.Contains(productName));
			return result;
		}

		public IQueryable<OrderItem> GetOrderItemByOrderId(long orderId)
		{
			var result = context.OrderItems.Where(p => p.OrderId == orderId);
			return result;
		}

	}
}
