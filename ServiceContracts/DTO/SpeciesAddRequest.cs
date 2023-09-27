

using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Act as a DTO class for adding new Species
	/// </summary>
	public class SpeciesAddRequest
	{
		[Required(ErrorMessage = "Species Name can not be blank!")]
		public string SpeciesName { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Converts the current object of SpeciesAddRequest into a new object of Species type
		/// </summary>
		/// <returns></returns>
		public Species MapToSpecies()
		{
			return new Species()
			{
				SpeciesName = SpeciesName,
				Description = Description
			};
		}
	}
}
