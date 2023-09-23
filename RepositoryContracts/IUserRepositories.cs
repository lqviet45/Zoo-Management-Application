using Entities.Models;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing User entity
	/// </summary>
	public interface IUserRepositories
	{
		/// <summary>
		/// Adds a User object to the data store
		/// </summary>
		/// <param name="user">The user to add</param>
		/// <returns>User obj after adding</returns>
		Task<User> Add(User user);

		/// <summary>
		/// Get all the user having ZooTrainer role in the dataset
		/// </summary>
		/// <returns>A list of User object</returns>
		Task<List<User>> GetAllZooTrainer();

		/// <summary>
		/// Get all the user having Staff role in the dataset
		/// </summary>
		/// <returns>A list of User object</returns>
		Task<List<User>> GetAllStaff();

		/// <summary>
		/// Get a staff by Id
		/// </summary>
		/// <param name="staffId">The Id of the staff</param>
		/// <returns>A matching User object</returns>
		Task<User?> GetStaffById(long staffId);

		/// <summary>
		/// Get a ZooTrainer by Id
		/// </summary>
		/// <param name="zooTrainerId">The Id of the ZooTrainer</param>
		/// <returns>A matching User object</returns>
		Task<User?> GetZooTrainerById(long zooTrainerId);
	}
}
