using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


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
