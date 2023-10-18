

using Entities.Models;

namespace RepositoryContracts
{
	public interface IAnimalUserRepositories
	{
		/// <summary>
		/// Adds a AnimalUser object to the data store
		/// </summary>
		/// <param name="animalUser">The user to add</param>
		/// <returns>AnimalUser obj after adding</returns>
		Task<AnimalUser> Add(AnimalUser animalUser);

		/// <summary>
		/// Get the relationship between animal and user
		/// </summary>
		/// <param name="animalId">The animal id</param>
		/// <param name="userId">The zoo trainer id</param>
		/// <returns>Return the matching AnimalUser obj</returns>
		Task<AnimalUser?> GetAnimalUserRelationship(long? animalId, long? userId);

		/// <summary>
		/// Get the trained animal of a zoo trainer
		/// </summary>
		/// <param name="userId">The id of the zoo trainer</param>
		/// <returns>A list of animalUser</returns>
		Task<List<AnimalUser>> GetAnimalByZooTrainerId(long? userId);

		/// <summary>
		/// Get all zoo trainer of an animal
		/// </summary>
		/// <param name="animalId">Animal Id</param>
		/// <returns>A list of AnimalUser</returns>
		Task<List<AnimalUser>> GetZooTrainerByAnimalId(long? animalId);

		/// <summary>
		/// Delete the relationship between animal and user
		/// </summary>
		/// <param name="animalId">The id of an animal</param>
		/// <param name="userId">The id of the zoo trainer</param>
		/// <returns></returns>
		Task<bool> Delete(long animalId, long userId);
		
		/// <summary>
		/// Get AnimalUser by animalUserId
		/// </summary>
		/// <param name="animalUserId">Animal User Id</param>
		/// <returns>Returns AnimalUser obj</returns>
		Task<AnimalUser?> GetAnimalUserByAnimalIdAndUserId(long animalUserId);
	}
}
