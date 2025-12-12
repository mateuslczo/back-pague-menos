using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PagMenos.Domain.Interfaces;

namespace PagMenos.Domain.Entities
{
	public record OrderItem :EntityBase
	{
        public OrderItem()
        {
            
        }

		[Required]
		public long OrderId { get; set; }

		[Required]
		public long ProductId { get; set; }

		[Required]
		public int ProductQuantity { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPrice => ProductQuantity * UnitPrice;

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; } = null!;

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; } = null!;
	}
}

