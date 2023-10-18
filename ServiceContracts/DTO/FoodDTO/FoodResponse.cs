using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.FoodDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Food service
	/// </summary>
	public class FoodResponse
	{
		public int FoodId { get; set; }

		[Required(ErrorMessage = "FoodName can not be blank!")]
		public string? FoodName { get; set; }
	}

	public static class FoodExtension
	{
		/// <summary>
		/// An extension method to convert an object of Food class into FoodResponse class
		/// </summary>
		/// <param name="person">The Food object to convert</param>
		/// /// <returns>Returns the converted FoodResponse object</returns>
		public static FoodResponse ToFoodResponse(this Food food)
		{
			return new FoodResponse()
			{
				FoodId = food.FoodId,
				FoodName = food.FoodName
			};
		}
	}

}
