using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Animal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		
		public long AnimalId { get; set; }

		[ForeignKey("Species")]
		[NotNull]
		public int SpeciesId { get; set; }

		[StringLength(80)]
		[NotNull]
		public string? AnimalName { get; set;}

		[Column(TypeName = "Date")]
		[NotNull]
		public DateTime DateArrive { get; set; }

		[StringLength(20)]
		[NotNull]
		public string? Status { get; set; }

		[NotNull]
		public bool IsDelete { get; set; }
		[NotNull]
		public virtual Species? Species { get; set; }
		

		public virtual ICollection<AnimalUser> AnimalZooTrainers { get; set; } = new List<AnimalUser>();

		public virtual ICollection<AnimalCage> AnimalCages { get; set; } = new List<AnimalCage>();
	}
}
