using Entities.Models;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for adding new Meal
	/// </summary>
	public class MealAddRequest
	{
		[Required(ErrorMessage ="Animal ID can't be blank")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "Food ID can't be blank")]
		public int FoodId { get; set; }
		public string? Note { get; set; }
		[Required(ErrorMessage = "Feeding Time can't be blank")]
		public DateTime FeedingTime { get; set; }


		/// <summary>
		/// Converts the current object of MealAddRequest into a new object of Meal type
		/// </summary>
		/// <returns></returns>
		public AnimalFood MealToAnimalFood()
		{
			return new AnimalFood()
			{
				AnimalId = AnimalId,
				FoodId = FoodId,
				Note = Note,
				FeedingTime = FeedingTime
			};
		}
	}
}
