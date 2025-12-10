using OrderDataManagement.Domain.Entities;

namespace OrderDataManagement.Domain.Interfaces
{
	public interface IOrderItemRepository : IGenericRepository<OrderItem>
	{
		/// <summary>
		/// Buscar item do pedido por nome do produto
		/// </summary>
		/// <param name="productName"></param>
		/// <returns>IQueryable<OrderItem></returns>
		IQueryable<OrderItem> GetOrderItemByProduct(string productName);

		/// <summary>
		/// Buscar item do pedido por nome do produto
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns>IQueryable<OrderItem></returns>
		IQueryable<OrderItem> GetOrderItemByOrderId(long orderId);

	}
}
