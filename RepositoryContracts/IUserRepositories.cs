using Entities.Models;
using System.Linq.Expressions;

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

		/// <summary>
		/// Get a user by userName
		/// </summary>
		/// <param name="userName">The userName to get</param>
		/// <returns>A matching user or null</returns>
		Task<User?> GetUserByUserName(string? userName);

		/// <summary>
		/// Get a user by Id
		/// </summary>
		/// <param name="id">the id of a user to get</param>
		/// <returns>A matching user or null</returns>
		Task<User?> GetUserById(long id);

		/// <summary>
		/// Returns all user object base on the given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>All matching persons with given condition</returns>
		Task<List<User>> GetFilteredUsers(Expression<Func<User, bool>> predicate);

		/// <summary>
		/// Update an existed user
		/// </summary>
		/// <param name="user">The user to update</param>
		/// <returns>A user after Updated</returns>
		Task<User> Update(User user);

		/// <summary>
		/// Detele an existed user by id
		/// </summary>
		/// <param name="userId">The id to delete</param>
		/// <returns>True if delete is success, else False</returns>
		Task<bool> Delete(long userId);
	}
}
