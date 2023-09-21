using Entities.AppDbContext;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();

			return Ok(user);
		}
	}
}
