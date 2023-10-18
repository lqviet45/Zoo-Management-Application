using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.FoodDTO
{
	/// <summary>
	/// Represents the DTO class that contains the Food details to update
	/// </summary>
	public class FoodUpdateRequest
	{
		[Required(ErrorMessage = "FoodId can not be blank!")]
		public int FoodId { get; set; }

		[Required(ErrorMessage = "Food Name Can not be blank!")]
		public string? FoodName { get; set; }

		/// <summary>
		/// Converts the current object of FoodAddRequest into a new object of Food type
		/// </summary>
		/// <returns>Returns Area object</returns>
		public Food MapToFood()
		{
			return new Food
			{
				FoodId = FoodId,
				FoodName = FoodName
			};
		}

	}
}
