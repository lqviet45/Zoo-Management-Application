using Entities.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for adding new Meal
	/// </summary>
	public class MealAddRequest
	{
	
		[Required(ErrorMessage ="Animal ID can't be blank")]
		public long AnimalUserId { get; set; }

		[Required(ErrorMessage = "Food ID can't be blank")]
		public int FoodId { get; set; }
		public string? Note { get; set; }
		[Required(ErrorMessage = "Feeding Time can't be blank")]
		public TimeSpan FeedingTime { get; set; }


		/// <summary>
		/// Converts the current object of MealAddRequest into a new object of Meal type
		/// </summary>
		/// <returns></returns>
		public AnimalFood MealToAnimalFood()
		{
			return new AnimalFood()
			{
				AnimalUserId = AnimalUserId,
				FoodId = FoodId,
				Note = Note,
				FeedingTime = FeedingTime
			};
		}
	}
}
