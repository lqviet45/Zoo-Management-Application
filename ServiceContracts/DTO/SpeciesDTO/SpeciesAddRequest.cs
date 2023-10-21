using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ServiceContracts.DTO.SpeciesDTO
{
    /// <summary>
    /// Act as a DTO class for adding new Species
    /// </summary>
    public class SpeciesAddRequest
    {
        [Required(ErrorMessage = "Species Name can not be blank!")]
        public string SpeciesName { get; set; } = string.Empty;

        [Required(ErrorMessage ="Family can not be blank!")]
        public string? Family { get; set; }

		[Required(ErrorMessage ="Information can not be blank!")]
		public string? Information { get; set; }

		[Required(ErrorMessage ="Characteristic can not be blank!")]
		public string? Characteristic { get; set; }

		[Required(ErrorMessage ="Allocation can not be blank!")]
		public string? Allocation { get; set; }

		[Required(ErrorMessage ="Ecological can not be blank!")]
		public string? Ecological { get; set; }

		[Required(ErrorMessage ="Diet can not be blank!")]
		public string? Diet { get; set; }

		[Required(ErrorMessage ="Breeding and reproduction can not be blank!")]
		public string? BreedingAndReproduction { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }

		/// <summary>
		/// Converts the current object of SpeciesAddRequest into a new object of Species type
		/// </summary>
		/// <returns></returns>
		public Species MapToSpecies()
        {
            return new Species()
            {
                SpeciesName = SpeciesName,
                Family = Family,
				Information = Information,
				Characteristic = Characteristic,
				Allocation = Allocation,
				Ecological = Ecological,
				Diet = Diet,
				BreedingAndReproduction = BreedingAndReproduction
            };
        }
    }
}
