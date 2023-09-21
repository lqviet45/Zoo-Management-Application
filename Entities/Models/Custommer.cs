using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Custommer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long CustommerId { get; set; }

		[NotNull]
		[StringLength(80)]
		public string? Name { get; set; }

		[NotNull]
		[StringLength(80)]
		public string? Email { get; set; }

		[NotNull]
		[StringLength(20)]
		public string? PhoneNumber { get; set; }
	}
}
