using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Species
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SpeciesId { get; set; }

		[StringLength(50)]
		[NotNull]
		public string? SpeciesName { get; set;}
		public string? Description { get; set;}

	}
}
