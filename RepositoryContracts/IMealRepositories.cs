

using Entities.Models;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing AnimalFood entity
	/// </summary>
	public interface IMealRepositories
	{
		/// <summary>
		/// Adds a AnimalFood object to the data store
		/// </summary>
		/// <param name="animalFood">The meal to add</param>
		/// <returns>AnimalFood obj after adding</returns>
		Task<AnimalFood> Add(AnimalFood animalFood);

		/// <summary>
		/// Get all meal of an animal
		/// </summary>
		/// <param name="id">The Id of the animal</param>
		/// <returns>List of meals or list of AnimalFood obj</returns>
		Task <List<AnimalFood>> GetAnimalMealById(long id);

		/// <summary>
		/// Get all meal of animals
		/// </summary>
		/// <returns>List of meals or list of AnimalFood obj</returns>
		Task<List<AnimalFood>> GetAllMeal();

		/// <summary>
		/// Update an existed meal
		/// </summary>
		/// <param name="animalFood">The AnimalFood obj to be Update</param>
		/// <returns>AnimalFood obj after Updated</returns>
		Task<AnimalFood> UpdateMeal(AnimalFood animalFood);

		/// <summary>
		/// Delete a food in an existed  meal
		/// </summary>
		/// <param name="animalFood">The AnimalFood obj to be delete</param>
		/// <returns>Returns true if delete is successful, otherwise returns false</returns>
		Task<bool> DeleteFoodInAMeal(AnimalFood animalFood);
	}
}
