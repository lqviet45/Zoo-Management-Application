using Entities.Models;
using ServiceContracts.DTO.AnimalUserDTO;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of AnimalFood service
	/// </summary>
	public class MealResponse
	{
		[Required]
		public long AnimalUserId { get; set; }
		public string? Note { get; set; }
		[Required]
		public TimeSpan FeedingTime { get; set; }

		public List<FoodResponse> Food { get; set; } = new List<FoodResponse>();

		[NotNull]
		public AnimalUserResponse? animalUser { get; set; }
	}

	public static class MealResponseExtensionMethods
	{
		/// <summary>
		/// An extension method to convert an object of AnimalFood class into MealResponse class
		/// </summary>
		/// <param name="meal">The AnimalFood object to convert</param>
		/// /// <returns>Returns the converted MealResponse object</returns>
		public static MealResponse MapToResponse(this AnimalFood meal)
		{
			return new MealResponse()
			{
				AnimalUserId = meal.AnimalUserId,
				Note = meal.Note,
				FeedingTime = meal.FeedingTime,
				animalUser = meal.AnimalUser.ToAnimalUserResponse()
				
			};
		}
	}

}
