using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AreaDTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Area service
    /// </summary>
    public class AreaResponse
    {
        public int AreaId { get; set; }

        [Required(ErrorMessage = "AreaName can not be blank!")]
        public string? AreaName { get; set; }
    }

    public static class AreaExtension
    {
        /// <summary>
        /// An extension method to convert an object of Area class into AreaResponse class
        /// </summary>
        /// <param name="area">The Area object to convert</param>
        /// /// <returns>Returns the converted AreaResponse object</returns>
        public static AreaResponse ToAreaResponse(this Area area)
        {
            return new AreaResponse()
            {
                AreaId = area.AreaId,
                AreaName = area.AreaName
            };
        }
    }

}

