using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrderDataManagement.Domain.Interfaces;

namespace OrderDataManagement.Domain.Entities
{
	public record Product :EntityBase
	{
		public Product()
		{

		}

		[Required]
		[StringLength(50)]
		public string ProductName { get; set; } = string.Empty;


		[StringLength(100)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public int StockQuantity { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

	}
}
