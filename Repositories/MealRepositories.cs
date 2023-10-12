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
		private readonly IAnimalUserRepositories _animalUserRepositories;

		// Constructor
		public MealRepositories(ApplicationDbContext dbContext, IFoodRepositories foodRepositories, IAnimalUserRepositories animalUserRepositories)
		{
			_dbContext = dbContext;
			_foodRepositories = foodRepositories;
			_animalUserRepositories = animalUserRepositories;	
		}
		public async Task<AnimalFood> Add(AnimalFood animalFood)
		{
			var animalUser = await _animalUserRepositories.GetAnimalUserRelationship(animalFood.AnimalUser.AnimalId, animalFood.AnimalUser.UserId);

			if(animalUser is null)
			{
				throw new ArgumentException("This Zoo Trainer don't take care of this animal");
			}

			var food = await _foodRepositories.GetFoodByFoodId(animalFood.FoodId);

			if(food == null) return animalFood;

			_dbContext.AnimalFoods.Add(animalFood);
			await _dbContext.SaveChangesAsync();

			animalFood.Food = food;
			animalFood.AnimalUser.Animal = animalUser.Animal;
			animalFood.AnimalUser.User = animalUser.User;

			return animalFood;
		}

		public async Task<bool> DeleteAMeal(long animalUserId, TimeSpan feedingTime)
		{
			var deleteFood = await _dbContext.AnimalFoods
								.Where(x => x.AnimalUserId == animalUserId
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
							.Where(x => x.AnimalUserId == animalFood.AnimalUserId
							 && x.FoodId == animalFood.FoodId
							 && x.FeedingTime == animalFood.FeedingTime)
							.FirstOrDefaultAsync();

			if(deleteFood == null) return false;

			 _dbContext.AnimalFoods.Remove(deleteFood);

			int isDeleted = await _dbContext.SaveChangesAsync();

			return isDeleted > 0; 

		}

		// haven't use yet 
		public async Task<List<AnimalFood>> GetAllMeal()
		{
			var listMeal = await _dbContext.AnimalFoods
							.Include(f => f.Food)
							.Include(a => a.AnimalUser.Animal)
							.Include(u => u.AnimalUser.User)
							.ToListAsync();
							

			return listMeal;
		}

		public async Task<AnimalFood?> GetSingleFoodInAnimalMeal(AnimalFood animalFood)
		{
			var matchingFood = await _dbContext.AnimalFoods.Include(f => f.Food).Include(a => a.AnimalUser.Animal).Include(u => u.AnimalUser.User)
								.Where(x => x.AnimalUserId == animalFood.AnimalUserId
								 && x.FoodId == animalFood.FoodId
								&& x.FeedingTime == animalFood.FeedingTime)
								.FirstOrDefaultAsync();

			return matchingFood;
		}

		public async Task<List<IGrouping<TimeSpan,AnimalFood>>> GetAnimalMealById(long id)
		{
			var listMeal = await _dbContext.AnimalFoods
								.Include(f => f.Food)
								.Include(a => a.AnimalUser.Animal)
								.Include(u => u.AnimalUser.User)
								.Where(meal => meal.AnimalUserId == id)
								.GroupBy(meal => meal.FeedingTime)
								.ToListAsync();

			return listMeal;
		}

		public async Task<List<AnimalFood>> GetAnimalMealInASpecifiedTime(long animaUserlId, TimeSpan feedingTime)
		{
			var meal = await _dbContext.AnimalFoods
								.Include(f => f.Food)
								.Include(a => a.AnimalUser.Animal)
								.Include(u => u.AnimalUser.User)
								.Where(meal => meal.AnimalUserId == animaUserlId
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
