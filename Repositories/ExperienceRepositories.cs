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

		public async Task<bool> Delete(int experienceId)
		{
			var experience = await _context.Experiences.FindAsync(experienceId);
			if (experience == null) return false;

			_context.Experiences.Remove(experience);

			var isDeleted = await _context.SaveChangesAsync();

			return isDeleted > 0;
		}

		public async Task<Experience?> GetExperienceById(int ExperienceId)
		{
			var experience = await _context.Experiences
				.Include(c => c.Skills)
				.FirstOrDefaultAsync(experience => experience.ExperienceId == ExperienceId);
			return experience; 
		}

		public async Task<Experience> Update(Experience experience)
		{
			var experienceExist = await GetExperienceById(experience.ExperienceId);
			if (experienceExist == null) return experience;

			//_context.Skills.UpdateRange(experience.Skills);
			experienceExist.Skills = experience.Skills;

			await _context.SaveChangesAsync();


			return experienceExist;
		}
	}
}
