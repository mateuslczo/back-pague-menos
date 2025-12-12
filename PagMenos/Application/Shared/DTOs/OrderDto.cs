using PagMenos.Domain.Enums;

namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record OrderDto
	{
		public OrderDto()
		{

		}

		public OrderDto(string orderNumber, long collaboratorId, decimal totalOrder)
		{
			OrderNumber = orderNumber;
			CollaboratorId = collaboratorId;
			TotalOrder = totalOrder;
		}

		public string OrderNumber { get; set; } = string.Empty;

		public long CollaboratorId { get; set; }

		public decimal TotalOrder { get; set; }

		public OrderStatusEnumDto OrderStatus { get; set; } = OrderStatusEnumDto.Pending;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public CollaboratorDto? Collaborator { get; init; } = null;

		public IReadOnlyCollection<OrderItemDto> OrderItems { get; init; } = Array.Empty<OrderItemDto>();

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
