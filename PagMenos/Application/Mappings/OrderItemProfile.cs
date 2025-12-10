using AutoMapper;
using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;

namespace PagMenos.Application.Mappings
{
	public class OrderItemProfile : Profile
	{
		public OrderItemProfile()
		{

			CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
			

		}
	}
}
