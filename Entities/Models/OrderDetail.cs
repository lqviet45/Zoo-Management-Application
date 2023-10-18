using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class OrderDetail
	{
		[ForeignKey("Order")]
		[NotNull]
		public long OrderID { get; set; }

		[ForeignKey("Ticket")]
		[NotNull]
		public int TicketId { get; set; }
		[NotNull]
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }
		public virtual Ticket? Ticket { get; set; }
		public virtual Order? Order { get; set; }
	}
}
