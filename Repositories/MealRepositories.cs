using Entities.Models;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Entities.AppDbContext;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Repositories
{
	public class MealRepositories : IMealRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;
		private readonly IFoodRepositories _foodRepositories;

		// Constructor
		public MealRepositories(ApplicationDbContext dbContext, IFoodRepositories foodRepositories)
		{
			_dbContext = dbContext;
			_foodRepositories = foodRepositories;
		}
		public async Task<AnimalFood> Add(AnimalFood animalFood)
		{
			var animal = await _dbContext.Animals.FindAsync(animalFood.AnimalId);

			var food = await _foodRepositories.GetFoodByFoodId(animalFood.FoodId);
			if(food == null || animal == null) return animalFood;

			_dbContext.AnimalFoods.Add(animalFood);
			await _dbContext.SaveChangesAsync();

			animalFood.Food = food;
			animalFood.Animal = animal;

			return animalFood;
		}

		public async Task<bool> DeleteAMeal(long animalId, TimeSpan feedingTime)
		{
			var deleteFood = await _dbContext.AnimalFoods.Where(x => x.AnimalId == animalId
													&& x.FeedingTime == feedingTime)
													.ToListAsync();
			if (deleteFood.IsNullOrEmpty())
			{
				return false;
			}

			_dbContext.AnimalFoods.RemoveRange(deleteFood);

			int isDeleted = await _dbContext.SaveChangesAsync();

			return isDeleted > 0;

		}

		public async Task<bool> DeleteFoodInAMeal(AnimalFood animalFood)
		{
			var deleteFood = await _dbContext.AnimalFoods
							.Where(x => x.AnimalId == animalFood.AnimalId
							 && x.FoodId == animalFood.FoodId
							 && x.FeedingTime == animalFood.FeedingTime)
							.FirstOrDefaultAsync();

			if(deleteFood == null) return false;

			 _dbContext.AnimalFoods.Remove(deleteFood);

			int isDeleted = await _dbContext.SaveChangesAsync();

			return isDeleted > 0; 

		}

		public async Task<List<AnimalFood>> GetAllMeal()
		{
			var listMeal = await _dbContext.AnimalFoods
							.Include(f => f.Food)
							.Include(a => a.Animal)
							.ToListAsync();
							

			return listMeal;
		}

		public async Task<AnimalFood?> GetAnimalFoodById(AnimalFood animalFood)
		{
			var matchingFood = await _dbContext.AnimalFoods.Include(f => f.Food).Include(a => a.Animal)
								.Where(x => x.AnimalId == animalFood.AnimalId
								 && x.FoodId == animalFood.FoodId
								&& x.FeedingTime == animalFood.FeedingTime)
								.FirstOrDefaultAsync();

			return matchingFood;
		}

		public async Task<List<AnimalFood>> GetAnimalMealById(long id)
		{
			var listMeal = await _dbContext.AnimalFoods.Include(f => f.Food).Include(a => a.Animal).
						  Where(meal => meal.AnimalId == id).ToListAsync();

			return listMeal;
		}

		public async Task<List<AnimalFood>> GetAnimalMealInASpecifiedTime(long animalId, TimeSpan feedingTime)
		{
			var meal = await _dbContext.AnimalFoods.Include(f => f.Food).Include(a => a.Animal)
								.Where(meal => meal.AnimalId == animalId
								&& meal.FeedingTime == feedingTime)
								.ToListAsync();

			return meal;
		}

		public Task<AnimalFood> UpdateMeal(AnimalFood animalFood)
		{

			throw new NotImplementedException();
		}
	}
}
