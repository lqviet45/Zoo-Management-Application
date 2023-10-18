using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
	public class AnimalCageRepositories : IAnimalCageRepositories
	{
		// Fields
		private readonly ApplicationDbContext _dbContext;
		private readonly IAnimalRepositories _animalRepositories;
		private readonly ICageRepositories _cageRepositories;

		//Contructor
		public AnimalCageRepositories(ApplicationDbContext dbContext, IAnimalRepositories animalRepositories, ICageRepositories cageRepositories)
		{
			_dbContext = dbContext;
			_animalRepositories = animalRepositories;
			_cageRepositories = cageRepositories;
		}

		public async Task<AnimalCage> Add(AnimalCage animalCage)
		{
			var animal = await _animalRepositories.GetAnimalById(animalCage.AnimalId);
			var cage = await _cageRepositories.GetCageById(animalCage.CageId);
			if (animal is null || cage is null)
			{
				throw new ArgumentException("Animal or cage is not exist");
			}

			_dbContext.AnimalCages.Add(animalCage);

			await _dbContext.SaveChangesAsync();

			return animalCage;
		}

		public async Task<List<AnimalCage>> GetAnimalCageHistory(long animalId)
		{
			var listAnimalCage = await _dbContext.AnimalCages.Where(animalCage => animalCage.AnimalId == animalId)
				.Include(animalCage => animalCage.Cage)
				.Include(animalCage => animalCage.Animal)
				.ToListAsync();
			return listAnimalCage;
		}

		public async Task<List<AnimalCage>> GetAllAnimalCage()
		{
			var listAnimalCage = await _dbContext.AnimalCages
				.Include(animalCage => animalCage.Cage)
				.Include(animalCage => animalCage.Animal)
				.Where(animalCage => animalCage.IsIn == true)
				.ToListAsync();
			return listAnimalCage;
		}

		public async Task<List<AnimalCage>> GetAllAnimalInTheCage(int cageId)
		{
			var listAnimalCage = await _dbContext.AnimalCages.Where(animalCage => animalCage.CageId == cageId && animalCage.IsIn == true)
				.Include(animalCage => animalCage.Cage)
				.Include(animalCage => animalCage.Animal)
				.ToListAsync();
			return listAnimalCage;
		}

		public async Task<bool> CheckAnimalCage(AnimalCage animalCage)
		{
			var animal = await _animalRepositories.GetAnimalById(animalCage.AnimalId);
			var cage = await _cageRepositories.GetCageById(animalCage.CageId);
			if (animal is null || cage is null)
			{
				throw new ArgumentException("Animal or cage is not exist");
			}

			var isExist = _dbContext.AnimalCages.FirstOrDefault
				(a => a.AnimalId == animalCage.AnimalId 
				 && a.CageId == animalCage.CageId
				 && a.DayIn == animalCage.DayIn
				 && a.IsIn == true);

			if (isExist != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public async Task<AnimalCage> GetAnimalPresentCage(long animalId)
		{
			var animalCage = await _dbContext.AnimalCages
				.Include(animalCage => animalCage.Cage)
				.Include(animalCage => animalCage.Animal)
				.Where(animalCage => animalCage.AnimalId == animalId && animalCage.IsIn == true)
				.FirstOrDefaultAsync();

			if (animalCage == null)
			{
				throw new ArgumentException("Animal is not in any cage");
			}

			return animalCage;
		}

		public async Task<bool> MoveAnimalOut(long animalId)
		{
			var animalCage = _dbContext.AnimalCages.FirstOrDefault(animalCage => animalCage.AnimalId == animalId && animalCage.IsIn == true);

			if (animalCage is null)
			{
				return false;
			}

			animalCage.IsIn = false;
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}
