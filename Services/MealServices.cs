using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.FoodDTO;
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
		public async Task<AnimalFoodResponse> AddMeal(List<MealAddRequest> mealAddRequest)
		{
			List<AnimalFood> animalFoods = new List<AnimalFood>();

			var mealResponse = new AnimalFoodResponse();
			mealResponse.AnimalUserId = mealAddRequest.First().AnimalUserId;
			mealResponse.Note = mealAddRequest.First().Note;
			mealResponse.FeedingTime = mealAddRequest.First().FeedingTime;

			foreach (var meal in mealAddRequest)
			{
				var animalFood = meal.MealToAnimalFood();
				await _mealRepositories.Add(animalFood);
				animalFoods.Add(animalFood);
			}

			animalFoods.ForEach(af =>
			{
				if (af.Food is not null)
					mealResponse.FoodId = af.FoodId;

			});

			return mealResponse;
		}

		public async Task<bool> DeleteAFoodInAMeal(MealDeleteRequest2 deleteFood)
		{
			var food = await _mealRepositories.GetSingleFoodInAnimalMeal(deleteFood.MealToAnimalFood());

			if (food is null) return false;

			var isDelete =  await _mealRepositories.DeleteFoodInAMeal(food);

			return isDelete;
		}

		public async Task<bool> DeleteAMeal(MealDeleteRequest deleteMeal)
		{
			var meal = await _mealRepositories
				.GetAnimalMealInASpecifiedTime(deleteMeal.AnimalUserId, deleteMeal.FeedingTime);

			if(meal is null) return false;

			var isDelete = await _mealRepositories.DeleteAMeal(deleteMeal.AnimalUserId, deleteMeal.FeedingTime);

			return isDelete;

		}

		public async Task<List<MealResponse>> GetAllMeal()
		{
			var listMeal = await _mealRepositories.GetAllMeal();

			List<MealResponse> listMealResponse = new List<MealResponse>();

			foreach (var meal in listMeal)
			{
				var mealResponse = meal.First().MapToResponse();
				mealResponse.Food = meal.Select(m => m.Food.ToFoodResponse()).ToList();
				listMealResponse.Add(mealResponse);
			}


			return listMealResponse;
		}

		public async Task<List<MealResponse>> GetAnimalMealById(long id)
		{
			var listMeal = await _mealRepositories.GetAnimalMealById(id);

			List<MealResponse> listMealResponse = new List<MealResponse>();

			foreach(var meal in listMeal)
			{
				var mealResponse = meal.First().MapToResponse();
				mealResponse.Food = meal.Select(m => m.Food.ToFoodResponse()).ToList();
				listMealResponse.Add(mealResponse);
			}
		

			return listMealResponse;
		}

	

		public async Task<List<FoodResponse>> GetAnimalMealByIdAndTime(long animalUserid, TimeSpan time)
		{
			var listFood = await _mealRepositories.GetAnimalMealInASpecifiedTime(animalUserid, time);

			var foodResponse = listFood.Select(m => m.MapToAnimalFoodResponse()).ToList();

			List<FoodResponse> foodList = new List<FoodResponse>();

			foodResponse.ForEach(food =>
			{
				var foodDetail = _foodRepositories.GetFoodByFoodId(food.FoodId).Result;

				if(foodDetail != null)
				{
					foodList.Add(foodDetail.ToFoodResponse());
				}

			});

			return foodList;
		}

	}
}
