using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


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
			user.IsDelete = false;
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();
			return user;
		}

		public async Task<bool> Delete(long userId)
		{
			var userDelete = await GetUserById(userId);
			if (userDelete is null)
			{
				return false;
			}
			userDelete.IsDelete = true;
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<List<User>> GetAllStaff()
		{
			var listStaff = await _dbContext.Users.Where(user => user.RoleId == 2 && user.IsDelete == false)
				.Include(u => u.Role)
				.ToListAsync();
			return listStaff;
		}

		public async Task<List<User>> GetAllZooTrainer()
		{
			var listZooTrainer = await _dbContext.Users.Where(user => user.RoleId == 3 && user.IsDelete == false)
				.Include(u => u.Role)
				.ToListAsync();
			return listZooTrainer;
		}

		public async Task<User?> GetStaffById(long staffId)
		{
			var matchingStaff = await _dbContext.Users.Include(u => u.Role)
				.FirstOrDefaultAsync(staff => staff.UserId == staffId && staff.IsDelete == false);
			
			return matchingStaff;
		}

		public async Task<User?> GetUserById(long id)
		{
			var matchingUser = await _dbContext.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(user => user.UserId == id && user.IsDelete == false);

			return matchingUser;
		}

		public Task<User?> GetUserByName(string? userName)
		{
		    return _dbContext.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(user => user.UserName == userName && user.IsDelete == false);
		}

		public async Task<User?> GetZooTrainerById(long zooTrainerId)
		{
			var matchingZooTrainer = await _dbContext.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(zooTrainer => zooTrainer.UserId == zooTrainerId && zooTrainer.IsDelete == false);

			return matchingZooTrainer;
		}

		public async Task<User> Update(User user)
		{
			var userUpdate = _dbContext.Users
				.FirstOrDefault(u => u.UserId == user.UserId);
			if (userUpdate is null)
			{
				return user;
			}
			userUpdate.UserName = user.UserName;
			userUpdate.FullName = user.FullName;
			userUpdate.Email = user.Email;
			userUpdate.Gender = user.Gender;
			userUpdate.PhoneNumber = user.PhoneNumber;
			userUpdate.DateOfBirth = user.DateOfBirth;
			userUpdate.IsDelete = user.IsDelete;

			await _dbContext.SaveChangesAsync();
			return userUpdate;
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
