using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.AuthenDTO;
using ServiceContracts.DTO.ExperienceDTO;
using ServiceContracts.DTO.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;
		private readonly IExperienceServices _experienceService;
		private readonly IConfiguration _configuration;
		private readonly IJwtServices _jwtServices;

		public UserController(IUserServices userServices, IExperienceServices experienceService, IConfiguration configuration, IJwtServices jwtServices)
		{
			_userServices = userServices;
			_experienceService = experienceService;
			_configuration = configuration;
			_jwtServices = jwtServices;
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(string username, string password)
		{
			var userLogin = await _userServices.LoginUser(username, password);

			if (userLogin is null || userLogin.Email is null || userLogin.Role is null)
			{
				return BadRequest("Username or password is not correct!");
			}

			//string token = CreateToken(userLogin);
			//AuthenticationResponse authenUser = new()
			//{
			//	UserName = userLogin.UserName,
			//	Email = userLogin.Email,
			//	Role = userLogin.Role.RoleName,
			//	Token = token,
			//	Expiration = DateTime.UtcNow.AddMinutes(10)
			//};

			var authenUser = _jwtServices.CreateToken(userLogin);

			return Ok(authenUser);
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

		private string CreateToken(UserResponse user)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, user.Role.RoleName)
			};

			var key = new SymmetricSecurityKey(
				System.Text.Encoding.UTF8.GetBytes(
					_configuration.GetSection("AppSettings:Token").Value
					));

			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: claims,		
				expires: DateTime.Now.AddDays(1),
				signingCredentials: cred);

			var jwt = new JwtSecurityTokenHandler()
				.WriteToken(token);

			return jwt;
		}
	}
}
