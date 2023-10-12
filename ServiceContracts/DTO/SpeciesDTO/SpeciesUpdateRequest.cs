using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceContracts.DTO.SpeciesDTO
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

		[Required(ErrorMessage = "Family can not be blank!")]
		public string? Family { get; set; }

		[Required(ErrorMessage = "Information can not be blank!")]
		public string? Infomation { get; set; }

		[Required(ErrorMessage = "Characteristic can not be blank!")]
		public string? Characteristic { get; set; }

		[Required(ErrorMessage = "Allocation can not be blank!")]
		public string? Allocation { get; set; }

		[Required(ErrorMessage = "Ecological can not be blank!")]
		public string? Ecological { get; set; }

		[Required(ErrorMessage = "Diet can not be blank!")]
		public string? Diet { get; set; }

		[Required(ErrorMessage = "Breeding and reproduction can not be blank!")]
		public string? BreedingAndReproduction { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }

		[Required]
		public bool? IsDeleted { get; set; }

		/// <summary>
		/// Converts the current object of SpeciesUpdateRequest into a new object of Cage type
		/// </summary>
		/// <returns>Returns Species object</returns>
		public Species MapToSpecies()
        {
            return new Species
            {
                SpeciesId = SpeciesId,
                SpeciesName = SpeciesName,
                Family = Family,
				Infomation = Infomation,
				Characteristic = Characteristic,
				Allocation = Allocation,
				Ecological = Ecological,
				Diet = Diet,
				BreedingAndReproduction = BreedingAndReproduction,
				IsDeleted = IsDeleted
            };
        }

    }
}
