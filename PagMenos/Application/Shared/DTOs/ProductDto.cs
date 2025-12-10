namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record ProductDto 
	{

        public ProductDto()
        {
            
        }

		public ProductDto(string productName, string description, int stockQuantity, decimal price)
		{
			ProductName = productName;
			Description = description;
			StockQuantity = stockQuantity;
			Price = price;
		}

		public string ProductName { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int StockQuantity { get; set; }

		public decimal Price { get; set; }

	}
}
