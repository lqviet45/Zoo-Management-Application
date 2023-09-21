using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Experience
	{
		[ForeignKey("User")]
		[NotNull]
		[Key]
		public long ZooTrainerId { get; set; }

		[NotNull]
		public int YearExp { get; set; }

		public virtual User? ZooTrainer { get; set; }
		public virtual ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();

	}
}
