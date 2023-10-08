using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalUserDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of AnimalUser service
	/// </summary>
	public class AnimalUserResponse
	{
		[Required(ErrorMessage = "AnimalId can not be blank!")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "UserId can not be blank!")]
		public long UserId { get; set; }

	}

	public static class AnimalUserExtension
	{
		/// <summary>
		/// An extension method to convert an object of Area class into AnimalUserResponse class
		/// </summary>
		/// <param name="animalUser">The AnimalUser object to convert</param>
		/// /// <returns>Returns the converted AnimalUserResponse object</returns>
		public static AnimalUserResponse ToAnimalUserResponse(this AnimalUser animalUser)
		{
			return new AnimalUserResponse()
			{
				AnimalId = animalUser.AnimalId,
				UserId = animalUser.UserId
			};
		}
	}
}
