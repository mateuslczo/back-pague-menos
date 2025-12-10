namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record CreateOrderItemDto
	{
		public CreateOrderItemDto()
		{
			TotalPriceCalculator();
		}


		public CreateOrderItemDto(long orderId, long productId, int productQuantity, decimal unitPrice)
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

		public decimal UnitPrice { get; set; } = 0;

		public decimal TotalPrice { get; set; }

		public void TotalPriceCalculator()
		{
			TotalPrice = ProductQuantity * UnitPrice;

		}
	}
}

