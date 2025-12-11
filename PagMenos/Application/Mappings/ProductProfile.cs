using PagMenos.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;
using AutoMapper;

namespace PagMenos.Application.Mappings
{

	public class ProductProfile :Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ReverseMap();	
		}
	}
}