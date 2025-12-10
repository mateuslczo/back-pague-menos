using Microsoft.EntityFrameworkCore;
using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;

namespace PagMenos.Application.Services
{

	public class OrderService :GenericService<Order>, IOrderService
	{
		private readonly IOrderRepository repository;

		public OrderService(IOrderRepository _repository) : base(_repository)
		{
			repository = _repository;
		}


		public async Task<Order?> GetOrderByNumber(string number)
		{
			var result = await repository.GetOrderByNumber(number).FirstOrDefaultAsync();

			return result;
		}

	}
}
