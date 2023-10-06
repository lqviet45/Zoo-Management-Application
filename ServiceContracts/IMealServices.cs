using ServiceContracts.DTO.MealDTO;

namespace ServiceContracts
{
	public interface IMealServices
	{
		/// <summary>
		/// Adding the new meal into the AnimalFood table
		/// </summary>
		/// <param name="mealAddRequest">The meal to add</param>
		/// <returns>MealResponse object base on the meal adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<MealResponse> AddMeal(List<MealAddRequest> mealAddRequest);

		/// <summary>
		/// Get All the meal in AnimalFood table base on animal Id
		/// </summary>
		/// <param name="id">The animal id </param>
		/// <returns>All the food that belong to an specified animal</returns>
		Task<List<MealResponse>> GetAnimalMealById(long id);

		/// <summary>
		/// Get animal food by animal id and date
		/// </summary>
		/// <param name="id">Animal ID</param>
		/// <param name="date">Date</param>
		/// <returns>The specified food belong to an animal at a specified time</returns>
		Task<List<MealResponse>> GetAnimalMealByIdAndDate(long id, DateTime date);

		Task<bool> DeleteAMeal(long id);
	}
}
