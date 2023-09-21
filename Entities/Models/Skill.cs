using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Skill
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SkillId { get; set; }

		[StringLength(40)]
		[NotNull]
		public string? SkillName { get; set; }

		public virtual ICollection<Experience> Experiences { get; set; } = new HashSet<Experience>();
	}
}
