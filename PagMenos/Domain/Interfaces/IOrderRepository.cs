using OrderDataManagement.Domain.Entities;

namespace OrderDataManagement.Domain.Interfaces
{
	public interface IOrderRepository :IGenericRepository<Order>
	{
		/// <summary>
		/// Buscar pedido por numerp
		/// </summary>
		/// <param name="number"></param>
		/// <returns>IQueryable<Order></returns>
		IQueryable<Order> GetOrderByNumber(string number);

	}
}
