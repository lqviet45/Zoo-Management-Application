using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.FoodDTO
{
	/// <summary>
	/// Act as a DTO class for adding new Food
	/// </summary>
	public class FoodAddRequest
	{
		[Required(ErrorMessage = "Food Name can not be blank!")]
		public string FoodName { get; set; } = string.Empty;

		/// <summary>
		/// Converts the current object of FoodAddRequest into a new object of Food type
		/// </summary>
		/// <returns></returns>
		public Food MapToFood()
		{
			return new Food()
			{
				FoodName = FoodName
			};
		}
	}

	
}
