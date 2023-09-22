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

		public async Task<List<UserResponse>> GetAllStaff()
		{
		 	var listStaff = await _userRepositories.GetAllStaff();
			var listStaffResponse = listStaff.Select(user => user.ToUserResponse()).ToList();
			return listStaffResponse;
		}

		public async Task<List<UserResponse>> GetAllZooTrainer()
		{
			var listZooTrainer = await _userRepositories.GetAllZooTrainer();
			var listZooTrainerresponse = listZooTrainer.Select(user => user.ToUserResponse()).ToList();
			return listZooTrainerresponse;
		}
	}
}
