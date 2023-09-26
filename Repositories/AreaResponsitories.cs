using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public Task<List<Area>> GetAllArea()
		{
			var listArea = _dbContext.Areas.ToListAsync();

			return listArea;
		}

		public Task<Area?> GetAreaById(int? areaId)
		{
			var area = _dbContext.Areas.Where(area => area.AreaId == areaId).FirstOrDefaultAsync();

			return area;
		}

		public Task<Area?> GetAreaByName(string areaName)
		{
			return _dbContext.Areas.Where(area => area.AreaName == areaName).FirstOrDefaultAsync();
		}

		public async Task<Area> UpdateArea(Area area)
		{
			Area? matchingArea = await _dbContext.Areas.FirstOrDefaultAsync(temp => temp.AreaId == area.AreaId);

			if(matchingArea == null) { return area; }

			matchingArea.AreaName = area.AreaName;
			matchingArea.IsDelete = area.IsDelete;

			int countUpdated = await _dbContext.SaveChangesAsync();

			return countUpdated > 0 ? matchingArea : area;
		}
	}
}
