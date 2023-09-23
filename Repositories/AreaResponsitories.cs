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
	}
}
