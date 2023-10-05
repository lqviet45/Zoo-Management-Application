using ServiceContracts.DTO.MealDTO;

namespace ServiceContracts
{
	public interface IMealServices
	{
		Task<MealResponse> AddMeal(List<MealAddRequest> mealAddRequest);

		Task<List<MealResponse>> GetAnimalMealById(long id);

	}
}
