using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;

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
		public async Task<ActionResult<List<UserResponse>>> GetAllStaff()
		{
			var listStaff = await _userServices.GetAllStaff();
			return Ok(listStaff);
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

		[HttpGet("search")]
		public async Task<ActionResult<List<UserResponse>>> GetFilteredStaff(string searchBy, string? searchString)
		{
			var listStaff = await _userServices.GetFiteredStaff(searchBy, searchString);
			return Ok(listStaff);
		}
	}
}
