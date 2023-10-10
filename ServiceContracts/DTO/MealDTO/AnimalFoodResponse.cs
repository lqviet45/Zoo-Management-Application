using Entities.Models;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of AnimalFood service
	/// </summary>
	public class AnimalFoodResponse
	{
		[Required]
		public long AnimalId { get; set; }

		[Required]
		public int FoodId { get; set; }

		public string? Note { get; set; }
		[Required]
		public TimeSpan FeedingTime { get; set; }

	}

	public static class AnimalFoodResponseExtensionMethods
	{
		/// <summary>
		/// An extension method to convert an object of AnimalFood class into AnimalFoodResponse class
		/// </summary>
		/// <param name="meal">The AnimalFood object to convert</param>
		/// /// <returns>Returns the converted MealResponse object</returns>
		public static AnimalFoodResponse MapToAnimalFoodResponse(this AnimalFood meal)
		{
			return new AnimalFoodResponse()
			{
				AnimalId = meal.AnimalId,
				FoodId = meal.FoodId,
				Note = meal.Note,
				FeedingTime = meal.FeedingTime
			};
		}
	}

}
