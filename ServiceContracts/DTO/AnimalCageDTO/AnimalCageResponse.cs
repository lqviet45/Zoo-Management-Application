using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalCageDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of AnimalCage service
	/// </summary>
	public class AnimalCageResponse
	{
		[Required(ErrorMessage = "CageId can not be blank!")]
		public int CageId { get; set; }

		[Required(ErrorMessage = "AnimalId can not be blank!")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "DateIn can not be blank!")]
		public DateTime? DayIn { get; set; }

		[Required(ErrorMessage = "IsIn can not be blank!")]
		public bool IsIn { get; set; }
	}

	public static class AnimalCageExtension
	{
		/// <summary>
		/// An extension method to convert an object of Area class into AnimalUserResponse class
		/// </summary>
		/// <param name="animalCage">The AnimalCage object to convert</param>
		/// /// <returns>Returns the converted AnimalCageResponse object</returns>
		public static AnimalCageResponse ToAnimalCageResponse(this AnimalCage animalCage)
		{
			return new AnimalCageResponse()
			{
				AnimalId = animalCage.AnimalId,
				CageId = animalCage.CageId,
				DayIn = animalCage.DayIn,
				IsIn = animalCage.IsIn
			};
		}
	}

}
