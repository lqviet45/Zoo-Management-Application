using ServiceContracts.DTO.MealDTO;

namespace ServiceContracts
{
	public interface IMealServices
	{
		Task<MealResponse> AddMeal(MealAddRequest mealAddRequest);

		Task<List<MealResponse?>> GetAnimalMealById(long id);

	}
}
