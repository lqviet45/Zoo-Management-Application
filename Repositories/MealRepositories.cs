using Entities.Models;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Entities.AppDbContext;

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

			animalFood.Food.Add(food);

			return animalFood;
		}

		public async Task<bool> DeleteFoodInAMeal(AnimalFood animalFood)
		{
			var deleteFood = await _dbContext.AnimalFoods.Where(x => x.AnimalId == animalFood.AnimalId
														 && x.FoodId == animalFood.FoodId)
														.FirstOrDefaultAsync();
			if(deleteFood == null) return false;

			 _dbContext.AnimalFoods.Remove(deleteFood);

			int isDeleted = await _dbContext.SaveChangesAsync();

			return isDeleted > 0; 

		}

		public async Task<List<AnimalFood>> GetAllMeal()
		{
			var listMeal = await _dbContext.AnimalFoods.ToListAsync();

			return listMeal;
		}

		public async Task<List<AnimalFood>> GetAnimalMealById(long id)
		{
			var listMeal = await _dbContext.AnimalFoods.Include(f => f.Food).
						  Where(meal => meal.AnimalId == id).ToListAsync();

			return listMeal;
		}

		public Task<AnimalFood> UpdateMeal(AnimalFood animalFood)
		{

			throw new NotImplementedException();
		}
	}
}
