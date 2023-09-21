using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helper;
using System;


namespace Services
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepositories _userRepositories;

		public UserServices(IUserRepositories userRepositories)
		{
			_userRepositories = userRepositories;
		}

		public async Task<UserResponse> AddUser(UserAddRequest? userAddRequest)
		{
			ArgumentNullException.ThrowIfNull(userAddRequest);

			ValidationHelper.ModelValidation(userAddRequest);

			User user = userAddRequest.MapToUser();

		    await _userRepositories.Add(user);

			return user.ToUserResponse();
		}

		public Task<List<UserResponse>> GetAllStaff()
		{
			throw new NotImplementedException();
		}

		public Task<List<UserResponse>> GetAllZooTrainer()
		{
			throw new NotImplementedException();
		}
	}
}
