using ServiceContracts.DTO.AuthenDTO;
using ServiceContracts.DTO.UserDTO;

namespace ServiceContracts
{
	public interface IJwtServices
	{
		AuthenticationResponse CreateToken(UserResponse user);
	}
}
