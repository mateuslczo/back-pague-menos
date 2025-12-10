using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using OrderDataManagement.Infrastructure.Data;

namespace OrderDataManagement.Infraestructure.Repositories
{

	public class OrderRepository :GenericRepository<Order>, IOrderRepository
	{
		private readonly EFDbContext context;

		public OrderRepository(EFDbContext _context) : base(_context)
		{
			context = _context;
		}


		public IQueryable<Order> GetOrderByNumber(string number)
		{
			var result = context.Orders.Where(p => p.OrderNumber == number);
			return result;
		}

	}
}
