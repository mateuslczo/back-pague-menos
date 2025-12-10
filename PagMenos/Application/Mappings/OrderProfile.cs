using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;
using AutoMapper;
using OrderDataManagement.Domain.Enums;

namespace PagMenos.Application.Mappings
{

	public class OrderProfile :Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, CreateOrderDto>()

				.ForMember(dest => dest.Status,
						   opt => opt.MapFrom(src => (OrderStatusEnumDto)src.OrderStatus))

				.ForMember(dest => dest.CollaboratorId,
						   opt => opt.MapFrom(src => src.Collaborator))

				.ForMember(dest => dest.OrderItems,
						   opt => opt.MapFrom(src => src.OrderItems))

				.ReverseMap()

				.ForMember(dest => dest.OrderStatus,
						   opt => opt.MapFrom(src => (OrderStatusEnum)src.Status))

				.ForMember(dest => dest.Collaborator, opt => opt.Ignore());

				//.ForMember(dest => dest.OrderItems, opt => opt.Ignore());
		}
	}
}