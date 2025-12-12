using PagMenos.Domain.Entities;

namespace PagMenos.Application.Interfaces.Services
{
	public interface IOrderItemService : IGenericService<OrderItem>
	{

		/// <summary>
		/// Buscar item do pedido por nome do produto
		/// </summary>
		/// <param name="productName"></param>
		/// <returns>IQueryable<OrderItem></returns>
		Task<List<OrderItem>> GetOrderItemByProduct(string productName);

		/// <summary>
		/// Buscar item do pedido por nome do produto
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns>IQueryable<OrderItem></returns>
		Task<OrderItem> GetOrderItemByOrderId(long orderId);
	}
}
