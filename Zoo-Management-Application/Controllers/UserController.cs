using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.ExperienceDTO;
using ServiceContracts.DTO.UserDTO;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;
		private readonly IExperienceServices _experienceService;

		public UserController(IUserServices userServices, IExperienceServices experienceService)
		{
			_userServices = userServices;
			_experienceService = experienceService;
		}

		
		[HttpPost]
		public async Task<ActionResult<UserResponse>> PostUser(UserAddRequest userAddRequest)
		{
			var userResponse = await _userServices.AddUser(userAddRequest);
			if (userAddRequest.ExperienceAddRequest != null)
			{
				userAddRequest.ExperienceAddRequest.UserId = userResponse.UserId;
				var experienceResponse = await _experienceService.AddExperience(userAddRequest.ExperienceAddRequest);
				userResponse.ExperienceResponses = new List<ExperienceResponse>() { experienceResponse };
			}

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

		[HttpPut]
		public async Task<ActionResult<UserResponse>> PutUser(UserUpdateRequest userUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var userUpdate = await _userServices.UpdateUser(userUpdateRequest);
				var experience = await _experienceService.AddExperience(userUpdateRequest.ExperienceAddRequest);
				userUpdate.ExperienceResponses.Add(experience);
				return Ok(userUpdate);
			}

			return NotFound("Can not update for some error");
		}

		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteUser(long userId)
		{
			var isDeleted = await _userServices.DeleteUser(userId);
			if (!isDeleted) return NotFound("Delete Fail by some error!!");

			return NoContent();
		}
	}
}
