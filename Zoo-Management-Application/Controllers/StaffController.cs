using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.DTO.WrapperDTO;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class StaffController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public StaffController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllStaff(int? pageNumber, string searchBy = "FullName", string? searchString = null)
		{
			var listStaff = await _userServices.GetFiteredStaff(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<UserResponse>.CreateAsync(listStaff.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetStaff(long Id)
		{
			var mathcingStaff = await _userServices.GetStaffById(Id);
			if (mathcingStaff == null || mathcingStaff.RoleId != 2)
			{
				return NotFound("The Staff Id dosen't exist!");
			}
			return Ok(mathcingStaff);
		}
	}
}
