using Entities.Models;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing AnimalCage entity
	/// </summary>
	public interface IAnimalCageRepositories
	{
		/// <summary>
		/// Adds a AnimalCage object to the data store (Add animal to a cage)
		/// </summary>
		/// <param name="animalCage">The AnimalCage to add</param>
		/// <returns>AnimalCage obj after adding</returns>
		Task<AnimalCage> Add(AnimalCage animalCage);

		/// <summary>
		/// Get all the cage that an animal has been in
		/// </summary>
		/// <param name="animalId">The id of the animal</param>
		/// <returns>List of animalcage</returns>
		Task<List<AnimalCage>> GetAnimalCageHistory(long animalId);

		/// <summary>
		/// Get all the cage that an animal is in
		/// </summary>
		/// <returns>List of animalcage</returns>
		Task<List<AnimalCage>> GetAllAnimalCage();

		/// <summary>
		/// Get all the animal in a cage
		/// </summary>
		/// <param name="cageId">The id of the cage</param>
		/// <returns>List of animalcage</returns>
		Task<List<AnimalCage>> GetAllAnimalInTheCage(int cageId);

		/// <summary>
		/// Check if the animal is in the cage at the same day or not
		/// </summary>
		/// <param name="animalCage">Obj of AnimalCage</param>
		/// <returns>Returns true if same, otherwise returns false</returns>
		Task<bool> CheckAnimalCage(AnimalCage animalCage);

		/// <summary>
		/// Get the cage that the animal is in
		/// </summary>
		/// <param name="animalId">The id of the cage</param>
		/// <returns>Returns the cage that animal is in</returns>
		Task<AnimalCage> GetAnimalPresentCage(long animalId);
	}
}
