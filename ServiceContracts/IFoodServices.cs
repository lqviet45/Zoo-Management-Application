using ServiceContracts.DTO.FoodDTO;	

namespace ServiceContracts
{
	public interface IFoodServices
	{
		/// <summary>
		/// Adding the new Food in to the Food table
		/// </summary>
		/// <param name="foodAddRequest">The food to add</param>
		/// <returns>FoodResponse object base on the food adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<FoodResponse> AddFood(FoodAddRequest foodAddRequest);

		/// <summary>
		/// Get All the Food in the Food table
		/// </summary>
		/// <returns>A list of Food object as FoodResponse</returns>
		Task<List<FoodResponse>> GetAllFood();

		/// <summary>
		/// Get a Food by Id
		/// </summary>
		/// <param name="foodId">The food Id to get</param>
		/// <returns>Matching food object as FoodResponse type</returns>
		Task<FoodResponse?> GetFoodById(int foodId);

		/// <summary>
		/// Get a Food by Name
		/// </summary>
		/// <param name="foodName">The food name to get</param>
		/// <returns>Matching food object as FoodResponse type</returns>
		Task<FoodResponse?> GetFoodByName(string foodName);

		/// <summary>
		/// Updates the specified food details based on the given food ID
		/// </summary>
		/// <param name="foodUpdateRequest">Food details to update</param>
		/// <returns>Returns the FoodResponse object updated</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		Task<FoodResponse> UpdateFood(FoodUpdateRequest foodUpdateRequest);

		/// <summary>
		/// Delete by Food id
		/// </summary>
		/// <param name="foodId">The food id to delete</param>
		/// <returns>True if delete success, else False</returns>
		Task<bool> DeleteFood(int foodId);

		/// <summary>
		/// Returns all food objects that matches with the given search field and search string
		/// </summary>
		/// <param name="searchBy">The field to search</param>
		/// <param name="searchString">The string to search</param>
		/// <returns>Returns all the matching food base on the given search field and search string</returns>
		Task<List<FoodResponse>> GetFilteredFood(string searchBy, string? searchString);
	}
}
