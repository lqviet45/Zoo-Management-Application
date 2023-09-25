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
	public class ExperienceRepositories : IExperienceRepositories
	{
		private readonly ApplicationDbContext _context;

		public ExperienceRepositories(ApplicationDbContext context) 
		{ 
			_context = context; 
		}

		public async Task<Experience> Add(Experience experience)
		{
			_context.Add(experience);
			await _context.SaveChangesAsync();
			return experience;
		}

		public async Task<List<Experience>> GetExperienceByUserId(long userId)
		{
			var experience = await _context.Experiences
				.Include(c => c.Skills)
				.Where(e => e.UserId == userId).ToListAsync();
			return experience;
		}
	}
}
