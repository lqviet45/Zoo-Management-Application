using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceContracts.DTO.MealDTO
{
	/// <summary>
	/// Act as a DTO class for deleting a Meal
	/// </summary>
	public class MealDeleteRequest
	{
		[Required(ErrorMessage ="Animal ID can't be blank")]
		public long AnimalId { get; set; }
		
		[Required(ErrorMessage = "FeedingTime can't be blank")]
		public TimeSpan FeedingTime { get; set; }
	}
}
