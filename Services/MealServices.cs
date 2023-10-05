﻿using Entities.Models;
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
		public async Task<MealResponse> AddMeal(List<MealAddRequest> mealAddRequest)
		{
			List<AnimalFood> animalFoods = new List<AnimalFood>();

			var mealResponse = new MealResponse();
			mealResponse.AnimalId = mealAddRequest.First().AnimalId;
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
					mealResponse.Foods.Add(af.Food.ToFoodResponse());

			});

			return mealResponse;
			//var food = await _foodRepositories.GetFoodByFoodId(mealAddRequest.FoodId);
			//if(food == null)
			//{
			//	throw new ArgumentNullException("The given food id doesn't exist!");
			//}
			//var meal = mealAddRequest.MealToAnimalFood();
			//await _mealRepositories.Add(meal);
			//return meal.MapToResponse();
 		}

		public async Task<List<MealResponse>> GetAnimalMealById(long id)
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
