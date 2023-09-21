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
	}
}
