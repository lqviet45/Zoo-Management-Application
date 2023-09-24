using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;

namespace Repositories
{
	public class UserRepositories : IUserRepositories
	{
		private readonly ApplicationDbContext _dbContext;

		//Contructor
		public UserRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<User> Add(User user)
		{
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();
			return user;
		}

		public async Task<List<User>> GetAllStaff()
		{
			var listStaff = await _dbContext.Users.Where(user => user.RoleId == 2)
				.Include("Experience").ToListAsync();

			listStaff.ForEach( user => {
				if (user.Experience != null)
				{
					user.Experience.Skills = _dbContext.Sp_GetUserSkill(user.ExperienceId);
				}
			});
			return listStaff;
		}

		public async Task<List<User>> GetAllZooTrainer()
		{
			var listZooTrainer = await _dbContext.Users.Where(user => user.RoleId == 3).Include("Experience").ToListAsync();
			listZooTrainer.ForEach(user => {
				if (user.Experience != null)
				{
					user.Experience.Skills = _dbContext.Sp_GetUserSkill(user.ExperienceId);
				}
			});
			return listZooTrainer;
		}

		public async Task<User?> GetStaffById(long staffId)
		{
			var matchingStaff = await _dbContext.Users
				.Include("Experience")
				.FirstOrDefaultAsync(staff => staff.UserId == staffId);
			if (matchingStaff?.Experience != null) { 
				matchingStaff.Experience.Skills = _dbContext.Sp_GetUserSkill(matchingStaff.ExperienceId);
			}
			return matchingStaff;
		}

		public Task<User?> GetUserByName(string? userName)
		{
		    return _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
		}

		public async Task<User?> GetZooTrainerById(long zooTrainerId)
		{
			var matchingZooTrainer = await _dbContext.Users
				.Include("Experience")
				.FirstOrDefaultAsync(zooTrainer => zooTrainer.UserId == zooTrainerId);
			if (matchingZooTrainer?.Experience != null)
			{
				matchingZooTrainer.Experience.Skills = _dbContext.Sp_GetUserSkill(matchingZooTrainer.ExperienceId);
			}
			return matchingZooTrainer;
		}

		//public  List<Skill> GetUserSkill(int? experienceId = -1)
		//{
		//	SqlParameter[] parameters = new SqlParameter[]
		//	{
		//		new SqlParameter("@experienceId", experienceId),

		//	};

		//	string sql = "select SkillId ,SkillName from Skills sk join ExperienceSkill es " +
		//		"on sk.SkillId = es.SkillsSkillId" +
		//		" join Experiences ex on es.ExperiencesExperienceId = ex.ExperienceId " +
		//		"where ex.ExperienceId = @experienceId";

		//	var listSkill =  _dbContext.Skills.FromSqlRaw<Skill>(sql, parameters).ToList();
		//	return listSkill;
		//}
	}
}
