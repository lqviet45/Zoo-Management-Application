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
	}
}
