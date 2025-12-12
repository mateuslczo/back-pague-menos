using PagMenos.Application.Shared.DTOs;
using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Domain.Entities;

namespace PagMenos.Application.Interfaces.Services
{
	public interface IOrderService :IGenericService<Order>
	{

		/// <summary>
		/// Cria um pedido
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		Task<int> CreateOrder(CreateOrderDto order);

		/// <summary>
		/// Recuperar lista e pedidos
		/// </summary>
		/// <param name="order"></param>
		/// <returns>Lista paginada de pedidos</returns>
		Task<PagedOrderResultDto> ListPagedOrder();

		/// <summary>
		/// Buscar pedido por numerp
		/// </summary>
		/// <param name="number"></param>
		/// <returns>IQueryable<Order></returns>
		Task<Order> GetOrderByNumber(string number);

	}
}
