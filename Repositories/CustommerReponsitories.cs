using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class CustommerReponsitories : ICustommerReponsitories
	{
		private readonly ApplicationDbContext _context;

        public CustommerReponsitories(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Custommer> AddCustommer(Custommer custommer)
		{
			_context.Custommers.Add(custommer);
			await _context.SaveChangesAsync();
			return custommer;
		}

		public async Task<List<Custommer>> GetCustommerByEmail(string email)
		{
			var custommerList = await _context.Custommers
				.Where(c => c.Email.Equals(email)).ToListAsync();

			return custommerList;
		}

		public async Task<Custommer?> GetCustommerById(long id)
		{
			var custommer = await _context.Custommers.FirstOrDefaultAsync(c => c.CustommerId == id);

			return custommer;
		}
	}
}
