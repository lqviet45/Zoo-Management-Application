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
			_dbContext.Cages.Add(cage);
			await _dbContext.SaveChangesAsync();
			// Dirty code
			cage = await GetCageById(cage.CageId);
			return cage;
		}

		public async Task<bool> DeleteCage(int AreaId)
		{
			_dbContext.Cages.RemoveRange(_dbContext.Cages.Where(temp => temp.CageId == AreaId));
			int rowDeleted = await _dbContext.SaveChangesAsync();

			return rowDeleted > 0;
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
			throw new NotImplementedException();
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
