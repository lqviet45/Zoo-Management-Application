using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace ServiceContracts.DTO.AnimalUserDTO
{
	/// <summary>
	/// Act as a DTO class for Updating new AnimalUser
	/// </summary>
	public class AnimalUserUpdateRequest
	{
		[Required(ErrorMessage = "AnimalId Can not be blank!")]
		public long UserId { get; set; }

		[Required(ErrorMessage = "UserId Can not be blank!")]
		public long AnimalId { get; set; }

		/// <summary>
		/// Converts the current object of AnimalUserUpdateRequest into a new object of AnimalUser type
		/// </summary>
		/// <returns></returns>
		public AnimalUser MapToAnimalUser()
		{
			return new AnimalUser()
			{
				AnimalId = this.AnimalId,
				UserId = this.UserId
			};
		}
	}
}
