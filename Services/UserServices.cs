using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helper;


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


			var userExist = await _userRepositories.GetUserByName(userAddRequest.UserName);
			if (userExist != null) {
				throw new ArgumentException("The userName is exist!");
			}

			ValidationHelper.ModelValidation(userAddRequest);

			User user = userAddRequest.MapToUser();

		    await _userRepositories.Add(user);

			return user.ToUserResponse();
		}

		public async Task<bool> DeleteUser(long userId)
		{
			var userExist = await _userRepositories.GetUserById(userId);
			if (userExist is null) return false;

			var isDeleted = await _userRepositories.Delete(userId);

			return isDeleted;
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

		public async Task<UserResponse?> GetStaffById(long staffId)
		{
			var matchingStaff = await _userRepositories.GetStaffById(staffId);

			if (matchingStaff is null) return null;

			return matchingStaff.ToUserResponse();
		}

		public async Task<UserResponse?> GetZooTrainerById(long zooTrainerId)
		{
			var matchingZooTrainer = await _userRepositories.GetZooTrainerById(zooTrainerId);
			if (matchingZooTrainer is null) return null;
			return matchingZooTrainer.ToUserResponse();
		}

		public async Task<UserResponse> UpdateUser(UserUpdateRequest? userUpdateRequest)
		{
			if(userUpdateRequest is null)
			{
				throw new ArgumentNullException(nameof(userUpdateRequest));
			}

			ValidationHelper.ModelValidation(userUpdateRequest);

			var userUpdate = await _userRepositories.GetUserById(userUpdateRequest.UserId);

			if (userUpdate is null)
			{
				throw new ArgumentException("Given user Id doesn't exist!");
			}

			userUpdate.UserName = userUpdateRequest.UserName;
			userUpdate.Email = userUpdateRequest.Email;
			userUpdate.Gender = userUpdateRequest.Gender;
			userUpdate.PhoneNumber = userUpdateRequest.PhoneNumber;
			userUpdate.DateOfBirth = userUpdateRequest.DateOfBirth;

			await _userRepositories.Update(userUpdate);

			return userUpdate.ToUserResponse();
		}
	}
}
