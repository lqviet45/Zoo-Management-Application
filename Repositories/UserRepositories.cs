using Entities.AppDbContext;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				.Include("Experience").Include("Role").ToListAsync();

			listStaff.ForEach( user => {
				if (user.Experience != null)
				{
					user.Experience.Skills = _dbContext.Sp_GetUserSkill(user.ExperienceId);
				}
			});
			return listStaff;
		}

		public Task<List<User>> GetAllZooTrainer()
		{
			var listZooTrainer = _dbContext.Users.Where(user => user.RoleId == 3).Include("Experience").ToListAsync();
			return listZooTrainer;
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
