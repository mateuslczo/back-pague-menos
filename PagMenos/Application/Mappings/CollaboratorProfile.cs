using AutoMapper;
using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;

namespace PagMenos.Application.Mappings
{

	public class CollaboratorProfile :Profile
	{
		public CollaboratorProfile()
		{
			CreateMap<Collaborator, CollaboratorDto>()
				.ReverseMap()

				.ForMember(dest => dest.Orders, opt => opt.Ignore());
		}
	}
}