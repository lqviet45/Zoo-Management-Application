using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO.SpeciesDTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Species service
    /// </summary>
    public class SpeciesResponse
    {
        public int SpeciesId { get; set; }
        [Required(ErrorMessage = "Species Name can not be blank!")]
        public string? SpeciesName { get; set; }

		[Required(ErrorMessage = "Family can not be blank!")]
		public string? Family { get; set; }

		[Required(ErrorMessage = "Information can not be blank!")]
		public string? Information { get; set; }

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

        [Required(ErrorMessage = "Image can not be blank!")]
		public string? Image { get; set; }

	}

    public static class SpeciesExtension
    {
        /// <summary>
        /// An extension method to convert an object of Species class into SpeciesResponse class
        /// </summary>
        /// <param name="person">The Species object to convert</param>
        /// /// <returns>Returns the converted SpeciesResponse object</returns>
        public static SpeciesResponse ToSpeciesResponse(this Species species)
        {
            return new SpeciesResponse()
            {
                SpeciesId = species.SpeciesId,
                SpeciesName = species.SpeciesName,
                Family = species.Family,
                Information = species.Information,
                Characteristic = species.Characteristic,
                Allocation = species.Allocation,
                Ecological = species.Ecological,
                Diet = species.Diet,
                BreedingAndReproduction = species.BreedingAndReproduction,
                Image = species.Image
               
            };
        }
    }

}
