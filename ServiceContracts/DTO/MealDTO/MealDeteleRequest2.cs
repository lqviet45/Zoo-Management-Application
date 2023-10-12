using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Entities.Models;
using Newtonsoft.Json;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for deleting a Meal
	/// </summary>
	public class MealDeleteRequest2
	{
		[Required(ErrorMessage = "Animal ID can't be blank")]
		public long AnimalUserId { get; set; }

		[Required(ErrorMessage = "FeedingTime can't be blank")]
		public TimeSpan FeedingTime { get; set; }

		[Required(ErrorMessage = "Food ID can't be blank")]
		public int FoodId { get; set; }

		/// <summary>
		/// Converts the current object of MealDeleteRequest2 into a new object of AnimalFood type
		/// </summary>
		/// <returns></returns>
		public AnimalFood MealToAnimalFood()
		{
			return new AnimalFood
			{
				AnimalUserId = AnimalUserId,
				FeedingTime = FeedingTime,
				FoodId = FoodId
			};
		}
	}
}
