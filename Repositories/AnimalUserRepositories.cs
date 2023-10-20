using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class AnimalUserRepositories : IAnimalUserRepositories
	{
		// private fields
		private readonly ApplicationDbContext _dbContext;
		private readonly IAnimalRepositories _animalRepositories;
		private readonly IUserRepositories _userRepositories;

		// constructor
		public AnimalUserRepositories(ApplicationDbContext dbContext, IAnimalRepositories animalRepositories, IUserRepositories userRepositories)
		{
			_dbContext = dbContext;
			_animalRepositories = animalRepositories;
			_userRepositories = userRepositories;
		}
		public async Task<AnimalUser> Add(AnimalUser animalUser)
		{
			var animal = _animalRepositories.GetAnimalById(animalUser.AnimalId);
			if(animal == null)
			{
				throw new Exception("Animal not found");
			}
			var user = _userRepositories.GetUserById(animalUser.UserId);
			if(user == null)
			{
				throw new Exception("User not found");
			}
			_dbContext.AnimalUsers.Add(animalUser);
			await _dbContext.SaveChangesAsync();

			return animalUser;
		}

		public async Task<bool> Delete(long animalId, long userId)
		{
			var deleteAnimalUser = await _dbContext.AnimalUsers.Where(x => x.AnimalId == animalId && x.UserId == userId)
									.FirstOrDefaultAsync();

			if(deleteAnimalUser == null) { return false; }

			_dbContext.AnimalUsers.Remove(deleteAnimalUser);

			int rowsDeleted = await _dbContext.SaveChangesAsync();

			return rowsDeleted > 0;
		}

		public async Task<AnimalUser?> GetAnimalUserRelationship(long? animalId, long? userId)
		{
			if(animalId == null || userId == null)
			{
				throw new Exception("AnimalId or UserId is null");
			}

			var animalUser = await _dbContext.AnimalUsers.FirstOrDefaultAsync
								(x => x.AnimalId == animalId 
								&& x.UserId == userId);

			return animalUser;
		}

		public Task<List<AnimalUser>> GetAnimalByZooTrainerId(long? userId)
		{
			var listAnimal = _dbContext.AnimalUsers.Where(x => x.UserId == userId)
								.Include(a => a.Animal).ToListAsync();

			return listAnimal;
		}

		public Task<List<AnimalUser>> GetZooTrainerByAnimalId(long? animalId)
		{
			var listZooTrainer = _dbContext.AnimalUsers.Where(x => x.AnimalId == animalId)
									.Include(u => u.User).ToListAsync();

			return listZooTrainer;
		}

		public Task<AnimalUser?> GetAnimalUserByAnimalIdAndUserId(long animalUserId)
		{
			var animalUser = _dbContext.AnimalUsers.Where(x => x.AnimalUserId == animalUserId)
								.Include(a => a.Animal)
								.Include(u => u.User)
								.FirstOrDefaultAsync();

			return animalUser;
		}
	}
}
