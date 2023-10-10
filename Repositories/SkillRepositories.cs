using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class SkillRepositories : ISkillRepositories
	{
		private readonly ApplicationDbContext _context;

        public SkillRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> Add(List<Skill> skills)
		{
			_context.Skills.AddRange(skills);
			await _context.SaveChangesAsync();

			return skills;
		}

		public async Task<bool> Delete(int skillId)
		{
			var existSkill = _context.Skills.FirstOrDefault(s => s.SkillId == skillId);
			if (existSkill is null)
			{
				return false;
			}
			_context.Skills.Remove(existSkill);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<List<Skill>> GetByUserId(long userId)
		{
			var listSkill = await _context.Skills.Where(s => s.UserId == userId).ToListAsync();

			return listSkill;
		}

		public async Task<Skill?> GetBySkillId(int skillId)
		{
			var listSkill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId);
			if(listSkill is null) return null;

			return listSkill;
		}

		public async Task<Skill> Update(Skill skills)
		{
			var skillExist = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == skills.SkillId);

			if (skillExist is null) return skills;

			skillExist.SkillName = skills.SkillName;
			
			await _context.SaveChangesAsync();

			return skills;
		}
	}
}
