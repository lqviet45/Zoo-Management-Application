using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IUserServices
	{
		/// <summary>
		/// Adding the new User in to the User table
		/// </summary>
		/// <param name="userAddRequest">The user to add</param>
		/// <returns>UserRespon object base on the user adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<UserResponse> AddUser(UserAddRequest? userAddRequest);

		/// <summary>
		/// Get All the User having ZooTrainer role in User table
		/// </summary>
		/// <returns>A list of User object as UserResponse</returns>
		Task<List<UserResponse>> GetAllZooTrainer();

		/// <summary>
		/// Get All the User having Staff role in User table
		/// </summary>
		/// <returns>A list of User object as UserResponse</returns>
		Task<List<UserResponse>> GetAllStaff();
	}
}
