using System.ComponentModel.DataAnnotations;

using Entities.Models;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for deleting a Meal
	/// </summary>
	public class MealDeleteRequest2
	{
		[Required(ErrorMessage = "Animal ID can't be blank")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "FeedingTime can't be blank")]
		public TimeSpan FeedingTime { get; set; }

		[Required(ErrorMessage = "Food ID can't be blank")]
		public int FoodId { get; set; }


		public AnimalFood MealToAnimalFood()
		{
			return new AnimalFood
			{
				AnimalId = AnimalId,
				FeedingTime = FeedingTime,
				FoodId = FoodId
			};
		}
	}
}
