using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long OrderId { get; set; }

		[NotNull]
		[Column(TypeName = "Date")]
		public DateTime PurchaseDate { get; set; }
		public double TotalPrice { get; set; }

		[ForeignKey("Custommer")]
		[NotNull]
		public long CustommerId { get; set; }
		public virtual Custommer? Custommer { get; set; }

		public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
	}
}
