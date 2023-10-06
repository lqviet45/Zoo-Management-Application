using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.MealDTO
{
	public class MealDeleteRequest
	{
		[Required(ErrorMessage ="Animal ID can't be blank")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "FeedingTime can't be blank")]
		public DateTime FeedingTime { get; set; }
	}
}
