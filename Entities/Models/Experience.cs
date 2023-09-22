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
		public int YearExp { get; set; }
		public virtual ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();

	}
}
