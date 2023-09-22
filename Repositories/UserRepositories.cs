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
			var listStaff = await _dbContext.Users.Where(user => user.RoleId == 2).ToListAsync();
			return listStaff;
		}

		public Task<List<User>> GetAllZooTrainer()
		{
			var listZooTrainer = _dbContext.Users.Where(user => user.RoleId == 3).Include("Experience").ToListAsync();
			return listZooTrainer;
		}
	}
}
