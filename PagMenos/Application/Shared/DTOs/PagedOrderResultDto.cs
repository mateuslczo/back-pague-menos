using PagMenos.Application.Shared.ExceptionsDTOs;

namespace PagMenos.Application.Shared.DTOs
{

	public class PagedOrderResultDto
	{
		public List<OrderDto>? orderItems { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
	}
}