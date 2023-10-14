using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.DTO.WrapperDTO;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin,OfficeStaff")]
	public class ZooTrainerController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public ZooTrainerController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllZooTrainer(int? pageNumber, string searchBy = "FullName", string? searchString = null)
		{
			var listZooTrainer = await _userServices.GetFiteredZooTrainer(searchBy, searchString);

			var pageSize = 5;
			var pagingList = PaginatedList<UserResponse>.CreateAsync(listZooTrainer.AsQueryable().AsNoTracking(),pageNumber ?? 1 , pageSize);

			var resopnse = new { pagingList, pagingList.TotalPages };

			return Ok(resopnse);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetZooTrainer(long Id)
		{
			var mathcingZooTrainer = await _userServices.GetZooTrainerById(Id);
			
			if (mathcingZooTrainer == null || mathcingZooTrainer.RoleId != 3)
			{
				return NotFound("The ZooTrainer Id dosen't exist!");
			}

			return Ok(mathcingZooTrainer);
		}
	}
}
