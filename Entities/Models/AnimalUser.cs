using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class AnimalUser
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long AnimalUserId { get; set; }

		[NotNull]
		public long AnimalId { get; set; }
		[NotNull]
		public long UserId { get; set; }

		public virtual Animal? Animal { get; set; }

		public virtual User? User { get; set; }
	}
}
