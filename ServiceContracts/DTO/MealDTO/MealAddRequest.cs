using Entities.Models;
using ServiceContracts.DTO.FoodDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for adding new Food
	/// </summary>
	public class MealAddRequest
	{
		[Required(ErrorMessage ="Animal ID can't be blank")]
		public long AnimalId { get; set; }

		public int FoodId { get; set; }
		public string? Note { get; set; }
		[Required(ErrorMessage = "Feeding Time can't be blank")]
		public DateTime FeedingTime { get; set; }

	
		
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
