using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Experience
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ExperienceId { get; set; }

		[NotNull]
		[ForeignKey("User")]
		public long UserId { get; set; }

		public virtual User? User { get; set; }

	}
}