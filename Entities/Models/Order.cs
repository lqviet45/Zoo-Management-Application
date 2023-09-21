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
		public DateTime PurchaseDate { get; set; }

		[ForeignKey("Custommer")]
		[NotNull]
		public long CustommerId { get; set; }
		public virtual Custommer? Custommer { get; set; }
	}
}
