using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


namespace Repositories
{
	public class CageRepositories : ICageRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;

		// constructor
		public CageRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<Cage> Add(Cage cage)
		{
			cage.IsDelete = false;
			_dbContext.Cages.Add(cage);
			await _dbContext.SaveChangesAsync();
			// Dirty code
			cage = await GetCageById(cage.CageId);
			return cage;
		}

		public async Task<bool> DeleteCage(int cageId)
		{
			var cageDelete = await GetCageById(cageId);
			if (cageDelete is null)
			{
				return false;
			}
			cageDelete.IsDelete = true;
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public Task<List<Cage>> GetAllCage()
		{
			var listCage = _dbContext.Cages.Include(cage => cage.Area)
										   .ToListAsync();
			return listCage;
		}

		public Task<Cage?> GetCageById(int? cageId)
		{
			var cage = _dbContext.Cages.Include(cage => cage.Area)
				.Where(cage => cage.CageId == cageId).FirstOrDefaultAsync();

			return cage;
		}

		public Task<Cage?> GetCageByName(string cageName)
		{
			return _dbContext.Cages.FirstOrDefaultAsync(cage => cage.CageName == cageName);
		}

		public async Task<Cage> UpdateCage(Cage cage)
		{
			Cage? matchingCage = await _dbContext.Cages.FirstOrDefaultAsync(temp => temp.CageId == cage.CageId);

			if(matchingCage == null) { return cage; }

			matchingCage.CageName = cage.CageName;
			matchingCage.IsDelete = cage.IsDelete;

			int countUpdated = await _dbContext.SaveChangesAsync();

			return countUpdated > 0 ? matchingCage : cage;
		}
	}
}
