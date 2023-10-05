using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalUserDTO
{
	/// <summary>
	/// Act as a DTO class for adding new AnimalUser
	/// </summary>
	public class AnimalUserAddRequest
	{
		[Required(ErrorMessage = "AnimalId Can not be blank!")]
		public long UserId { get; set; }

		[Required(ErrorMessage = "UserId Can not be blank!")]
		public long AnimalId { get; set; }

		/// <summary>
		/// Converts the current object of AnimalUserAddRequest into a new object of AnimalUser type
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
