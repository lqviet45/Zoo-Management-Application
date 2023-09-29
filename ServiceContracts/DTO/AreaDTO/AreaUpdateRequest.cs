using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AreaDTO
{
    /// <summary>
    /// Represents the DTO class that contains the Area details to update
    /// </summary>
    public class AreaUpdateRequest
    {
        [Required(ErrorMessage = "AreaId can not be blank!")]
        public int AreaId { get; set; }

        [Required(ErrorMessage = "Area Name Can not be blank!")]
        public string? AreaName { get; set; }

        public bool IsDelete { get; set; }

        /// <summary>
        /// Converts the current object of AreaAddRequest into a new object of Area type
        /// </summary>
        /// <returns>Returns Area object</returns>
        public Area MapToArea()
        {
            return new Area
            {
                AreaId = AreaId,
                AreaName = AreaName,
                IsDelete = IsDelete
            };
        }
    }
}
