using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.AuthenDTO;
using ServiceContracts.DTO.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
	public class JwtServices : IJwtServices
	{
		private readonly IConfiguration _configuration;
		public JwtServices(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public AuthenticationResponse CreateToken(UserResponse user)
		{
			user.Email ??= string.Empty;
			if (user.Role is null) 
				throw new ArgumentNullException("User don't have any role");

			DateTime expriration = DateTime.Now.AddDays
				(Convert.ToDouble(_configuration["Jwt:EXPIRATION_DAYS"]));

			List<Claim> claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, user.Role.RoleName),

				new Claim(ClaimTypes.NameIdentifier, user.UserName)
			};

			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));

			SigningCredentials signingCredentials = new SigningCredentials(
				securityKey, SecurityAlgorithms.HmacSha256Signature);

			JwtSecurityToken tokenGenerator = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims: claims,
				expires: expriration,
				signingCredentials: signingCredentials
				);

			JwtSecurityTokenHandler handler = new();

			string token = handler.WriteToken(tokenGenerator);

			return new AuthenticationResponse() 
			{
				UserId = user.UserId,
				Token = token,
				Email = user.Email,
				UserName = user.UserName,
				Expiration = expriration,
				Role = user.Role.RoleName
			};
		}
	}
}
