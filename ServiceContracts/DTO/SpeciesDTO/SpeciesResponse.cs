using Entities.Models;
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
                SpeciesName = species.SpeciesName
            };
        }
    }

}
