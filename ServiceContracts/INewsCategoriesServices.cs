using ServiceContracts.DTO.NewsCategoriesDTO;

namespace ServiceContracts
{
	public interface INewsCategoriesServices
	{
		/// <summary>
		/// Adding the new News Category into the User table
		/// </summary>
		/// <param name="newsCategoriesAddRequest">The News Category to add</param>
		/// <returns>NewsCategoryResponse object base on the Category adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<NewsCategoryResponse> AddNewsCategories(NewsCategoryAddRequest? newsCategoriesAddRequest);

		/// <summary>
		/// Get all the News Categories in News Categories table
		/// </summary>
		/// <returns>A list of NewsCategoris obj </returns>
		Task<List<NewsCategoryResponse>> GetAllNewsCategories();

		/// <summary>
		/// Get News Category by Id
		/// </summary>
		/// <param name="categoryId">The category id to get</param>
		/// <returns>Returns a matching category</returns>
		Task<NewsCategoryResponse?> GetCategoryById(int categoryId);

		/// <summary>
		/// Updates the specified  News Category details based on the given category ID
		/// </summary>
		/// <param name="newsCategoriesUpdateRequest"> News Category details to update</param>
		/// <returns>Returns the NewsCategories object updated</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		Task<NewsCategoryResponse> UpdateNewsCategories(NewsCategoryUpdateRequest? newsCategoriesUpdateRequest);

		/// <summary>
		/// Delete by Category id
		/// </summary>
		/// <param name="categoryId">The category id to delete</param>
		/// <returns>Returns True if delete success, else False</returns>
		Task<bool> DeleteCategory(int categoryId);
	}


}
