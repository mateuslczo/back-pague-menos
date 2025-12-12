using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.DTOs;
using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Domain.Entities;
using PagMenos.Domain.Interfaces;

namespace PagMenos.Application.Services
{

	public class OrderService :GenericService<Order>, IOrderService
	{
		private readonly IOrderRepository repository;
		private readonly IMapper mapper;

		public OrderService(IOrderRepository _repository, IMapper _mapper) : base(_repository)
		{
			repository = _repository;
			mapper = _mapper;
		}



		public async Task<int> CreateOrder(CreateOrderDto order)
		{

			order.GenerateOrderNumberIfEmpty();

			var orderMapped = mapper.Map<Order>(order);

			await AddAsync(orderMapped);

			var rowsAffected = await CommitAsync();

			return rowsAffected;

		}

		public async Task<PagedOrderResultDto> ListPagedOrder()
		{

			var listOrder = await PaginatedListAsync(includes: new[] { "Collaborator", "OrderItems" });

			var orderMappedList = mapper.Map<PagedOrderResultDto>(listOrder);

			return orderMappedList;

		}



		public async Task<Order?> GetOrderByNumber(string number)
		{
			var result = await repository.GetOrderByNumber(number).FirstOrDefaultAsync();

			return result;
		}

	}
}
