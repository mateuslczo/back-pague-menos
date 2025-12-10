namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record OrderItemDto
	{
		public OrderItemDto()
		{
			TotalPrice = ProductQuantity * UnitPrice;
		}

		public OrderItemDto(long orderId, long productId, int productQuantity, decimal unitPrice)
		{
			OrderId = orderId;
			ProductId = productId;
			ProductQuantity = productQuantity;
			UnitPrice = unitPrice;
			TotalPrice = productQuantity * unitPrice;
		}

		public long OrderId { get; set; }

		public long ProductId { get; set; }

		public int ProductQuantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TotalPrice { get; init; }

		//public OrderDto? Order { get; init; } = null;

		//public ProductDto? Product { get; init; } = null;
	}
}

