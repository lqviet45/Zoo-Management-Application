using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.MealDTO;

namespace Services
{
	public class MealServices : IMealServices
	{
		// private field
		private readonly IMealRepositories _mealRepositories;
		private readonly IFoodRepositories _foodRepositories;

		// Constructor
		public MealServices(IMealRepositories mealRepositories, IFoodRepositories foodRepositories)
		{
			_mealRepositories = mealRepositories;
			_foodRepositories = foodRepositories;
		}
		public async Task<MealResponse> AddMeal(MealAddRequest mealAddRequest)
		{
			var food = await _foodRepositories.GetFoodByFoodId(mealAddRequest.FoodId);
			if(food == null)
			{
				throw new ArgumentNullException("The given food id doesn't exist!");
			}
			var meal = mealAddRequest.MealToAnimalFood();
			await _mealRepositories.Add(meal);
			return meal.MapToResponse();
 		}

		public async Task<List<MealResponse?>> GetAnimalMealById(long id)
		{
			var listMeal = await _mealRepositories.GetAnimalMealById(id);
			if(listMeal is null)
			{
				return null;
			}

			return listMeal.Select(m => m.MapToResponse()).ToList();
		}
	}
}
