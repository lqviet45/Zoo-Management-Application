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
			_dbContext.Areas.Add(area);
			await _dbContext.SaveChangesAsync();
			return area;
		}

		public async Task<bool> DeleteArea(int AreaId)
		{
			_dbContext.Areas.RemoveRange(_dbContext.Areas.Where(temp => temp.AreaId == AreaId));
			int rowDeleted = await _dbContext.SaveChangesAsync();

			return rowDeleted > 0;
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
			throw new NotImplementedException();
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
