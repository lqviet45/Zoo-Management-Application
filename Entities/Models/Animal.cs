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

		[ForeignKey("User")]
		[NotNull]
		public long ZooTrainerId { get; set; }

		[StringLength(80)]
		[NotNull]
		public string? AnimalName { get; set;}

		[Column(TypeName = "DateTime2")]
		[NotNull]
		public DateTime DateArrive { get; set; }

		[StringLength(20)]
		[NotNull]
		public string? Status { get; set; }

		[NotNull]
		public bool IsDelete { get; set; }

		[ForeignKey("Cage")]
		[NotNull]
		public int CageId { get; set; }
		public virtual Cage? Cage { get; set; }
		public virtual Species? Species { get; set; }

		public virtual ICollection<User> ZooTrainers { get; set; } = null!;
	}
}
