using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public StaffController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllStaff(int? pageNumber, string searchBy = "FullName", string? searchString = null)
		{
			var listStaff = await _userServices.GetFiteredStaff(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<UserResponse>.CreateAsync(listStaff.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{UserId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<User>), Arguments = new object[] { "UserId", typeof(long) })]
		[Authorize(Roles = "Admin,OfficeStaff")]
		public async Task<IActionResult> GetStaff(long UserId)
		{
			var mathcingStaff = await _userServices.GetStaffById(UserId);
			if (mathcingStaff == null || mathcingStaff.RoleId != 2)
			{
				return NotFound("The Staff Id dosen't exist!");
			}
			return Ok(mathcingStaff);
		}
	}
}
