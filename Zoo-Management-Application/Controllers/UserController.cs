using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.AuthenDTO;
using ServiceContracts.DTO.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;
		private readonly IConfiguration _configuration;
		private readonly IJwtServices _jwtServices;
		private readonly ISkillServices _skillServices;

		public UserController(IUserServices userServices, IConfiguration configuration, IJwtServices jwtServices, ISkillServices skillServices)
		{
			_userServices = userServices;
			_configuration = configuration;
			_jwtServices = jwtServices;
			_skillServices = skillServices;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> Login(LoginUserDTO loginUser)
		{
			var userLogin = await _userServices.LoginUser(loginUser.UserName, loginUser.Password);

			if (userLogin is null || userLogin.Email is null || userLogin.Role is null)
			{
				return BadRequest("Username or password is not correct!");
			}

			var authenUser = _jwtServices.CreateToken(userLogin);

			return Ok(authenUser);

		}

		[HttpPost]
		[Authorize(Roles = "Admin,OfficeStaff")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<UserResponse>> PostUser(UserAddRequest userAddRequest)
		{

			var userResponse = await _userServices.AddUser(userAddRequest);

			foreach (var skill in userAddRequest.Skills)
			{
				skill.UserId = userResponse.UserId;
			}

			var listSkill = await _skillServices.AddSkills(userAddRequest.Skills);

			userResponse.skills = listSkill;

			var routeValues = new { UserId = userResponse.UserId };
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
		[Authorize(Roles = "Admin,OfficeStaff,ZooTrainer")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<UserResponse>> PutUser(UserUpdateRequest userUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var userUpdate = await _userServices.UpdateUser(userUpdateRequest);

				if (userUpdateRequest.Skills.Count > 0)
				{
					userUpdateRequest.Skills.ForEach(s =>
					{
						s.UserId = userUpdate.UserId;
					});

					var listSkillUpdate = await _skillServices.UpdateSkills(userUpdateRequest.Skills);

					userUpdate.skills = listSkillUpdate;
				}

				return Ok(userUpdate);
			}

			return NotFound("Can not update for some error");
		}

		[HttpDelete("{UserId}")]
		[Authorize(Roles = "Admin,OfficeStaff")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<User>), Arguments = new object[] { "UserId", typeof(long) })]
		public async Task<IActionResult> DeleteUser(long UserId)
		{
			var isDeleted = await _userServices.DeleteUser(UserId);
			if (!isDeleted) return NotFound("Delete Fail by some error!!");

			return NoContent();
		}

		[HttpPut("password")]
		[Authorize(Roles = "Admin,OfficeStaff,ZooTrainner")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> PutPassword(long userId, string oldPassword, string newPassword, string confirmPassword)
		{
			if (string.IsNullOrEmpty(oldPassword))
			{
				throw new ArgumentException($"'{nameof(oldPassword)}' cannot be null or empty.", nameof(oldPassword));
			}

			if (string.IsNullOrEmpty(newPassword))
			{
				throw new ArgumentException($"'{nameof(newPassword)}' cannot be null or empty.", nameof(newPassword));
			}

			if (newPassword != confirmPassword)
			{
				throw new ArgumentException($"{nameof(confirmPassword)} is not match the new password!");
			}
			var user = await _userServices.ChangePassword(userId, oldPassword, newPassword);

			return Ok(user);
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUser(long userId)
		{
			var user = await _userServices.GetUserById(userId);
			if (user == null)
			{
				return NotFound("The User does not exist!!");
			}
			return Ok(user);
		}
	}
}