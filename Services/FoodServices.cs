using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.FoodDTO;
using Services.Helper;

namespace Services
{
	public class FoodServices : IFoodServices
	{
		// private field
		private readonly IFoodRepositories _foodRepositories;

		// constructor
		public FoodServices(IFoodRepositories foodRepositories)
		{
			_foodRepositories = foodRepositories;
		}
		public async Task<FoodResponse> AddFood(FoodAddRequest foodAddRequest)
		{
			ArgumentNullException.ThrowIfNull(foodAddRequest);

			// Check Duplicate FoodName
			var foodExist = await _foodRepositories.GetFoodByName(foodAddRequest.FoodName);
			if (foodExist != null)
			{
				throw new ArgumentException("The FoodName is exist!");
			}

			ValidationHelper.ModelValidation(foodAddRequest);

			Food food = foodAddRequest.MapToFood();

			await _foodRepositories.Add(food);

			return food.ToFoodResponse();
		}

		public async Task<bool> DeleteFood(int foodId)
		{
			var foodExist = await _foodRepositories.GetFoodByFoodId(foodId);
			if (foodExist is null)
			{
				return false;
			}

			var isDeleted = await _foodRepositories.Delete(foodId);

			return isDeleted;
		}

		public async Task<List<FoodResponse>> GetAllFood()
		{
			var listFood = await _foodRepositories.GetAllFood();
			var listFoodResponse = listFood.Select(food => food.ToFoodResponse()).ToList();
			return listFoodResponse;
		}

		public async Task<List<FoodResponse>> GetFilteredFood(string searchBy, string? searchString)
		{
			if(string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<Food> foods = searchBy switch
			{
				nameof(FoodResponse.FoodName) =>
				await _foodRepositories.GetFiteredFood(temp =>
					temp.FoodName.Contains(searchString)),

				_ => await _foodRepositories.GetAllFood()
			};

			return foods.Select(food => food.ToFoodResponse()).ToList();

		}

		public async Task<FoodResponse?> GetFoodById(int foodId)
		{
			var matchingFood = await _foodRepositories.GetFoodByFoodId(foodId);

			if (matchingFood is null) { return null; }

			return matchingFood.ToFoodResponse();
		}

		public async Task<FoodResponse?> GetFoodByName(string foodName)
		{
			var matchingFood = await _foodRepositories.GetFoodByName(foodName);
			if (matchingFood is null) { return null; }
			return matchingFood.ToFoodResponse();
		}

		public async Task<FoodResponse> UpdateFood(FoodUpdateRequest foodUpdateRequest)
		{
			if (foodUpdateRequest is null)
			{
				throw new ArgumentNullException(nameof(foodUpdateRequest));
			}

			ValidationHelper.ModelValidation(foodUpdateRequest);

			var matchingFood = await _foodRepositories.GetFoodByFoodId(foodUpdateRequest.FoodId);

			if(matchingFood is null)
			{
				throw new ArgumentException("The FoodId is not exist!");
			}

			matchingFood.FoodName = foodUpdateRequest.FoodName;

			await _foodRepositories.Update(matchingFood);

			return matchingFood.ToFoodResponse();

		}
	}
}
