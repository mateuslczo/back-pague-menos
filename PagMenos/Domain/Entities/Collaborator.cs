using System.ComponentModel.DataAnnotations;
using OrderDataManagement.Domain.Interfaces;

namespace OrderDataManagement.Domain.Entities
{
	public record Collaborator :EntityBase
	{

		public Collaborator()
		{
		}

		[Required]
		[StringLength(50)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string User { get; set; } = string.Empty;

		[Required]
		[StringLength(255)]
		public string Password { get; set; } = string.Empty;

		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
	}
}