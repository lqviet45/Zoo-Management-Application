using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Ticket
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TicketId { get; set; }

		[StringLength(80)]
		[NotNull]
		public string? TicketName { get; set;}

		[NotNull]
		public double Price { get; set; }

		[NotNull]
		[Column(TypeName = "DateTime2")]
		public DateTime ReleaseDate { get; set; }
	}
}
