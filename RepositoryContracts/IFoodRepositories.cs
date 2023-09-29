using Entities.Models;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing Food entity
	/// </summary>
	public interface IFoodRepositories
	{
		/// <summary>
		/// Adds a Food object to the data store
		/// </summary>
		/// <param name="food">The food to add</param>
		/// <returns>Food obj after adding</returns>
		Task<Food> Add(Food food);

		/// <summary>
		/// Get all the food in the dataset
		/// </summary>
		/// <returns>A list of Food obj</returns>
		Task<List<Food>> GetAllFood();

		/// <summary>
		/// Get food by id in the dataset
		/// </summary>
		/// <param name="foodId">The id of the food</param>
		/// <returns>Matching food</returns>
		Task<Food?> GetFoodByFoodId(int foodId);

		/// <summary>
		/// Get food by name in the dataset
		/// </summary>
		/// <param name="foodName">The name of the food</param>
		/// <returns>Matching food</returns>
		Task<Food?> GetFoodByName(string foodName);

		/// <summary>
		/// Update an existed food
		/// </summary>
		/// <param name="food">The food obj to update</param>
		/// <returns>The updated food obj</returns>
		Task<Food> Update(Food food);

		/// <summary>
		/// Delete an existed food 
		/// </summary>
		/// <param name="foodId">The id of the food to delete</param>
		/// <returns>Returns true if delete successful, otherwise returns false</returns>
		Task<bool> Delete(int foodId);
	}
}
