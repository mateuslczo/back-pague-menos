using OrderDataManagement.Domain.Entities;

namespace PagMenos.Application.Interfaces.Services
{
	public interface IOrderService : IGenericService<Order>
	{


		/// <summary>
		/// Buscar pedido por numerp
		/// </summary>
		/// <param name="number"></param>
		/// <returns>IQueryable<Order></returns>
		Task<Order> GetOrderByNumber(string number);

	}
}
