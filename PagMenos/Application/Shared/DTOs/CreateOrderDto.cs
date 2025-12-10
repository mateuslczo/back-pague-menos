using OrderDataManagement.Domain.Enums;

namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record CreateOrderDto
	{
		public CreateOrderDto()
		{

		}


		public long CollaboratorId { get; set; }
		public string OrderNumber { get; set; } = string.Empty;
		public OrderStatusEnumDto Status { get; set; } = OrderStatusEnumDto.Created;
		public IReadOnlyCollection<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();

		public void GenerateOrderNumberIfEmpty()
		{
			if (string.IsNullOrEmpty(OrderNumber))
			{
				OrderNumber = OrderNumberGenerated().ToString();
			}
		}

		public long OrderNumberGenerated()
		{
			var random = new Random();
			return random.Next(100, 999);
		}
	}

}
