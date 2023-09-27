using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.ExperienceDTO;
using ServiceContracts.DTO.UserDTO;
using Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ZooTrainerController : ControllerBase
	{
		private readonly IUserServices _userServices;
		private readonly IExperienceServices _experienceServices;

		public ZooTrainerController(IUserServices userServices, IExperienceServices experienceServices)
		{
			_userServices = userServices;
			_experienceServices = experienceServices;
		}

		[HttpGet]
		public async Task<ActionResult<UserResponse>> GetAllZooTrainer()
		{
			var listZooTrainer = await _userServices.GetAllZooTrainer();

			return Ok(listZooTrainer);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetZooTrainer(long Id)
		{
			var mathcingZooTrainer = await _userServices.GetZooTrainerById(Id);
			
			if (mathcingZooTrainer == null || mathcingZooTrainer.RoleId != 3)
			{
				return NotFound("The ZooTrainer Id dosen't exist!");
			}

			mathcingZooTrainer.ExperienceResponses = await _experienceServices.GetExperienceByUserId(Id);

			return Ok(mathcingZooTrainer);
		}

		[HttpGet("experience/{userId}")]
		public async Task<IActionResult> GetExperience(long userId)
		{
			var experiences = await _experienceServices.GetExperienceByUserId(userId);
			if (experiences.IsNullOrEmpty()) return NotFound();
			return Ok(experiences);
		}

		[HttpDelete("experience/{experienceId}")]
		public async Task<IActionResult> DeleteExperience(int experienceId)
		{
			var isDelete = await _experienceServices.DeleteExperience(experienceId);
			if (isDelete) return NoContent();

			return NotFound("Can not delete experience by some error!");
		}

		[HttpPut("experience")]
		public async Task<IActionResult> PutExperience(ExperienceUpdateRequest experienceUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var experienceUpdate = await _experienceServices.UpdateExperience(experienceUpdateRequest);
				return Ok(experienceUpdate);
			}

			return NotFound("Can not update for some error!");

		}
	}
}
