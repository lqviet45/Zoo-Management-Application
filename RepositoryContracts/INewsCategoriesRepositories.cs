using Entities.Models;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing NewsCategories entity
	/// </summary>
	public interface INewsCategoriesRepositories
	{
		/// <summary>
		/// Adds a NewsCategories object to the data store
		/// </summary>
		/// <param name="newsCategories">The Category to add</param>
		/// <returns>Area obj after adding</returns>
		Task<NewsCategories> Add(NewsCategories newsCategories);

		/// <summary>
		/// Get all the Categories in the dataset
		/// </summary>
		/// <returns>A list of NewsCategories obj</returns>
		Task<List<NewsCategories>> GetAllCategories();

		/// <summary>
		/// Get a NewsCategories obj based on the given name
		/// </summary>
		/// <param name="name">name to search</param>
		/// <returns>Matching Name</returns>
		Task<NewsCategories?> GetCategoryByName(string name);

		/// <summary>
		/// Updates a NewsCategories obj based on the given NewsCategories obj
		/// </summary>
		/// <param name="newsCategories">NewsCategories obj to update </param>
		/// <returns>Returns the updated NewsCategories object</returns>
		Task<NewsCategories> UpdateCategory(NewsCategories newsCategories);

		/// <summary>
		/// Deletes a NewsCategories obj based on the given categoryId
		/// </summary>
		/// <param name="categoryId">Category ID to delete</param>
		/// <returns>Returns true if the deletion is successful otherwise false</returns>
		Task<bool> DeleteCategory(int categoryId);

		/// <summary>
		/// Get News Category by Id
		/// </summary>
		/// <param name="cateId">The News Category Id to get</param>
		/// <returns>Matching NewsCategories obj </returns>
		Task<NewsCategories?> GetCategoryById(int cateId);
	}
}
