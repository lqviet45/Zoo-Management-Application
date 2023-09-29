using Entities.Models;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	public class MealResponse
	{
		public long AnimalId { get; set; }

		[Required]
		public List<FoodResponse> Foods { get; set; } = new List<FoodResponse>();
		public string? Note { get; set; }
		[Required]
		public DateTime FeedingTime { get; set; }
	}

	public static class MealResponseExtensionMethods
	{
		public static MealResponse MapToResponse(this AnimalFood meal)
		{
			return new MealResponse()
			{
				AnimalId = meal.AnimalId,
				Foods = meal.Food.Select(f => f.ToFoodResponse()).ToList(),
				Note = meal.Note,
				FeedingTime = meal.FeedingTime
			};
		}
	}

}
