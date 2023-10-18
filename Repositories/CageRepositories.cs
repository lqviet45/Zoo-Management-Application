using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
	public class CageRepositories : ICageRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;
		private readonly IAreaRepositories _areaRepositories;

		// constructor
		public CageRepositories(ApplicationDbContext dbContext, IAreaRepositories areaRepositories)
		{
			_dbContext = dbContext;
			_areaRepositories = areaRepositories;
		}
		public async Task<Cage> Add(Cage cage)
		{	var Area =  await _areaRepositories.GetAreaById(cage.AreaId);
			if(Area is null) throw new ArgumentException("The Area is deleted!");
			cage.IsDelete = false;
			_dbContext.Cages.Add(cage);
			await _dbContext.SaveChangesAsync();
			// Dirty code
			cage.Area = Area;
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

		public async Task<List<Cage>> GetAllCage()
		{
			var listCage = await _dbContext.Cages.Include(cage => cage.Area)
									.Where(cage => cage.IsDelete == false)
									.ToListAsync();
										   
			return listCage;
		}

		public async Task<List<Cage>> GetCageByAreaId(int areaId)
		{
			var listCage = await _dbContext.Cages.Include(cage => cage.Area).
							Where(cage => cage.AreaId == areaId 
							&& cage.IsDelete == false)
							.ToListAsync();

			return listCage;
		}

		public async Task<Cage?> GetCageById(int? cageId)
		{
			var cage = await _dbContext.Cages.Include(cage => cage.Area)
				.Where(cage => cage.CageId == cageId && cage.IsDelete == false)
				.FirstOrDefaultAsync();

			return cage;
		}

		public async Task<Cage?> GetCageByName(string cageName)
		{
			return await _dbContext.Cages.FirstOrDefaultAsync(cage => 
						cage.CageName == cageName && cage.IsDelete == false);
		}

		public async Task<List<Cage>> GetFilteredCage(Expression<Func<Cage, bool>> predicate)
		{
			var listCage = await _dbContext.Cages.Include(cage => cage.Area)
									.Where(predicate).ToListAsync();

			return listCage;

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
