

using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Represents the DTO class that contains the Species details to update
	/// </summary>
	public class SpeciesUpdateRequest
	{
		[Required(ErrorMessage = "SpeciesId can not be blank!")]
		public int SpeciesId { get; set; }

		[Required(ErrorMessage = "Species Name can not be blank!")]
		public string? SpeciesName { get; set; }

		public string? Description { get; set; }

		/// <summary>
		/// Converts the current object of SpeciesAddRequest into a new object of Cage type
		/// </summary>
		/// <returns>Returns Species object</returns>
		public Species MapToSpecies()
		{
			return new Species
			{
				SpeciesId = SpeciesId,
				SpeciesName = SpeciesName,
				Description = Description
			};
		}

	}
}
