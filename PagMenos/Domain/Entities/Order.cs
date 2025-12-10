using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrderDataManagement.Domain.Enums;
using OrderDataManagement.Domain.Interfaces;

namespace OrderDataManagement.Domain.Entities
{
	public record Order :EntityBase
	{
		public Order()
		{

		}

		[Required]
		[StringLength(50)]
		public string OrderNumber { get; set; } = string.Empty;

		[Required]
		public long CollaboratorId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalOrder { get; set; }

		[Required]
		[StringLength(20)]
		public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Pending;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[ForeignKey("CollaboratorId")]
		public virtual Collaborator Collaborator { get; set; } = new Collaborator();

		public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
