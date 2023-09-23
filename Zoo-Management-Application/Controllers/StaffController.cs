using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

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

		[HttpGet("{staffId}")]
		public async Task<IActionResult> GetStaff(long staffId)
		{
			var mathcingStaff = await _userServices.GetStaffById(staffId);
			if (mathcingStaff == null)
			{
				return NotFound("The Staff Id dosen't exist!");
			}
			return Ok(mathcingStaff);
		}

		//[HttpPut]
		//public Task<ActionResult<UserResponse>> PutStaff(UserUpdateRequest userUpdateRequest)
		//{
			
		//}
	}
}
