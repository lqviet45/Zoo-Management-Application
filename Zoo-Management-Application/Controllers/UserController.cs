using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public UserController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		//[Bind(nameof(UserAddRequest.UserName), nameof(UserAddRequest.Email)
		//	, nameof(UserAddRequest.PhoneNumber), nameof(UserAddRequest.Gender), nameof(UserAddRequest.Password), nameof(UserAddRequest.ConfirmPassword)
		//	, nameof(UserAddRequest.RoleId), nameof(UserAddRequest.Experience))]
		[HttpPost]
		public async Task<ActionResult<UserResponse>> PostUser(UserAddRequest userAddRequest)
		{
			var userResponse = await _userServices.AddUser(userAddRequest);

			var routeValues = new { Id = userResponse.UserId };
			if (userResponse.RoleId == 2)
			{
				return CreatedAtAction("GetStaff", "Staff", routeValues, userResponse);
			}
            else if (userResponse.RoleId == 3)
            {
				return CreatedAtAction("GetZooTrainer", "ZooTrainer", routeValues, userResponse);
			}
			return NoContent();
		}
	}
}
