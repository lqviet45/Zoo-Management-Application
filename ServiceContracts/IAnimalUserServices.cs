using ServiceContracts.DTO.AnimalUserDTO;

namespace ServiceContracts
{
	public interface IAnimalUserServices
	{
		/// <summary>
		/// Adding the new AnimalUser into the AnimalUser table
		/// </summary>
		/// <param name="animalUserAddRequest">The AnimalUser to add</param>
		/// <returns>AnimalUserResponse object base on the animaluser adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<AnimalUserResponse> AddAnimalUser(AnimalUserAddRequest animalUserAddRequest);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="animalId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<bool> DeleteAnimalUser(long animalId, long userId);

		/// <summary>
		/// Get all animals that the zoo trainer is training
		/// </summary>
		/// <param name="userId">The zoo trainer id</param>
		/// <returns>A list of AnimalUserResponse</returns>
		Task<List<AnimalUserResponse>> GetAnimalByZooTrainerId(long? userId);

		/// <summary>
		/// Get all zoo trainer of an animal
		/// </summary>
		/// <param name="animalId">The id of an animal</param>
		/// <returns>A list of AnimalUserResponse</returns>
		Task<List<AnimalUserResponse>> GetZooTrainerByAnimalId(long? animalId);
	}
}
