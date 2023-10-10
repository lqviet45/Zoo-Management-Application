using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


namespace Repositories
{
	public class AreaRepositories : IAreaRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;

		// constructor
		public AreaRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Area> Add(Area area)
		{
			area.IsDelete = false;
			_dbContext.Areas.Add(area);
			await _dbContext.SaveChangesAsync();
			return area;
		}

		public async Task<bool> DeleteArea(int AreaId)
		{
			var areaDelete = await GetAreaById(AreaId);
            if (areaDelete is null)
            {
				return false;
            }
			areaDelete.IsDelete = true;
			await _dbContext.SaveChangesAsync();

			return areaDelete.IsDelete;
        }

		public async Task<List<Area>> GetAllArea()
		{
			var listArea = await _dbContext.Areas.Where(temp => temp.IsDelete == false).ToListAsync();

			return listArea;
		}

		public async Task<Area?> GetAreaById(int? areaId)
		{
			var area = await _dbContext.Areas.Where(area => area.AreaId == areaId && area.IsDelete == false).FirstOrDefaultAsync();

			return area;
		}

		public async Task<Area?> GetAreaByName(string areaName)
		{
			return await _dbContext.Areas.Where(area => area.AreaName == areaName && area.IsDelete == false).FirstOrDefaultAsync();
		}

		public async Task<Area> UpdateArea(Area area)
		{
			Area? matchingArea = await _dbContext.Areas
								.FirstOrDefaultAsync(temp => temp.AreaId == area.AreaId);

			if(matchingArea == null) { return area; }

			matchingArea.AreaName = area.AreaName;
			matchingArea.IsDelete = area.IsDelete;

			int countUpdated = await _dbContext.SaveChangesAsync();

			return countUpdated > 0 ? matchingArea : area;
		}
	}
}
