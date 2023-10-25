using Microsoft.AspNetCore.Http;
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

		[MaxLength]
		[NotNull]
		public string? SpeciesName { get; set;}

		[StringLength(100)]
		[NotNull]
		public string? Family { get; set; }

		[NotNull]
		[MaxLength]
		public string? Information { get; set; }

		[NotNull]
		[MaxLength]
		public string? Characteristic { get; set; }

		[NotNull]
		[MaxLength]
		public string? Allocation { get; set; }

		[NotNull]
		[MaxLength]
		public string? Ecological { get; set; }

		[NotNull]
		[MaxLength]
		public string? Diet { get; set; }

		[NotNull]
		[MaxLength]
		public string? BreedingAndReproduction { get; set; }
		[MaxLength]
		public string? Image { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }

		[NotNull]
		public bool IsDeleted { get; set; }
	}
}
