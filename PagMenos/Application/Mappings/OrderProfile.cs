using PagMenos.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;
using AutoMapper;
using PagMenos.Domain.Enums;
using PagMenos.Infraestructure.DataContexts;
using PagMenos.Application.Shared.DTOs;

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

			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
				.ReverseMap();

			CreateMap<OrderItem, OrderItemDto>().ReverseMap();

			CreateMap<PagedResult<Order>, PagedOrderResultDto>()
				.ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.Items))
				.ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
				.ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
				.ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
				.ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages))
				.ReverseMap();


		}
	}
}