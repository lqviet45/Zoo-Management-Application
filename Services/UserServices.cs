using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
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


			var userExist = await _userRepositories.GetUserByUserName(userAddRequest.UserName);
			if (userExist != null)
			{
				throw new ArgumentException("The userName is exist!");
			}

			ValidationHelper.ModelValidation(userAddRequest);

			User user = userAddRequest.MapToUser();
			await _userRepositories.Add(user);

			return user.ToUserResponse();
		}

		public async Task<UserResponse> ChangePassword(long userId, string oldPassword, string newPassword)
		{
			var user = await _userRepositories.GetUserById(userId);

			if (user == null)
			{
				throw new ArgumentException("User is not valid");
			}

			if (user.Password != oldPassword)
			{
				throw new ArgumentException("The password is not correct!");
			}
			user.Password = newPassword;
			var updateUser = await _userRepositories.ChangePassword(user);

			return updateUser.ToUserResponse();
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

		public async Task<List<UserResponse>> GetFiteredStaff(string searchBy, string? searchString)
		{
			if (string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<User> users = searchBy switch
			{
				nameof(UserResponse.FullName) =>
				await _userRepositories.GetFilteredUsers(temp =>
					temp.FullName.Contains(searchString) && temp.RoleId == 2 && temp.IsDelete == false),

				nameof(UserResponse.Email) =>
				await _userRepositories.GetFilteredUsers(temp =>
					temp.Email.Contains(searchString) && temp.RoleId == 2 && temp.IsDelete == false),

				nameof(UserResponse.PhoneNumber) => 
				await _userRepositories.GetFilteredUsers(temp =>
					temp.PhoneNumber.Contains(searchString) && temp.RoleId == 2 && temp.IsDelete == false),

				_ => await _userRepositories.GetAllStaff()
			};

			return users.Select(user => user.ToUserResponse()).ToList();
		}

		public async Task<List<UserResponse>> GetFiteredZooTrainer(string searchBy, string? searchString)
		{
			if (string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<User> users = searchBy switch
			{
				nameof(UserResponse.FullName) =>
				await _userRepositories.GetFilteredUsers(temp =>
					temp.FullName.Contains(searchString) && temp.RoleId == 3 && temp.IsDelete == false),

				nameof(UserResponse.Email) =>
				await _userRepositories.GetFilteredUsers(temp =>
					temp.Email.Contains(searchString) && temp.RoleId == 3 && temp.IsDelete == false),

				nameof(UserResponse.PhoneNumber) =>
				await _userRepositories.GetFilteredUsers(temp =>
					temp.PhoneNumber.Contains(searchString) && temp.RoleId == 3 && temp.IsDelete == false),

				_ => await _userRepositories.GetAllZooTrainer()
			};

			return users.Select(user => user.ToUserResponse()).ToList();
		}

		public async Task<UserResponse?> GetStaffById(long staffId)
		{
			var matchingStaff = await _userRepositories.GetStaffById(staffId);

			if (matchingStaff is null) return null;

			if (matchingStaff.RoleId != 2) return null;

			return matchingStaff.ToUserResponse();
		}

		public async Task<UserResponse?> GetZooTrainerById(long zooTrainerId)
		{
			var matchingZooTrainer = await _userRepositories.GetZooTrainerById(zooTrainerId);
			if (matchingZooTrainer is null) return null;
			if (matchingZooTrainer.RoleId != 3) return null;
			return matchingZooTrainer.ToUserResponse();
		}

		public async Task<UserResponse?> GetUserById(long userId)
		{
			var user = await _userRepositories.GetUserById(userId);
			if (user is null) return null;
			return user.ToUserResponse();
		}

		public async Task<UserResponse?> LoginUser(string userName, string password)
		{
			var user = await _userRepositories.GetUserLogin(userName, password);
			if (user is null) return null;
			return user.ToUserResponse();
		}

		public async Task<UserResponse> UpdateUser(UserUpdateRequest? userUpdateRequest)
		{
			if (userUpdateRequest is null)
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
			userUpdate.FullName = userUpdateRequest.FullName;
			userUpdate.PhoneNumber = userUpdateRequest.PhoneNumber;
			userUpdate.DateOfBirth = userUpdateRequest.DateOfBirth;

			await _userRepositories.Update(userUpdate);

			return userUpdate.ToUserResponse();
		}
	}
}
