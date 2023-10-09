

using ServiceContracts.DTO.AnimalCageDTO;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.CageDTO;

namespace ServiceContracts
{
	public interface IAnimalCageServices
	{
		/// <summary>
		/// Adding the Animal into the Cage
		/// </summary>
		/// <param name="animalCageAddRequest">The Animal and cage to add</param>
		/// <returns>AnimalCageResponse object base on the animal and cage</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<AnimalCageResponse> Add(AnimalCageAddRequest animalCageAddRequest);

		/// <summary>
		/// Get all the cage that an animal has been in
		/// </summary>
		/// <param name="animalId">The Id of an animal</param>
		/// <returns>A list of AnimalCageResponse</returns>
		Task<List<CageResponse>> GetAnimalCageHistory(long animalId);

		/// <summary>
		/// Get all the Cage that an animal has been in
		/// </summary>
		/// <returns>Returns list of animal and cage</returns>
		Task<List<AnimalCageResponse>> GetAllAnimalCage();

		/// <summary>
		/// Get all the animal in a cage
		/// </summary>
		/// <param name="cageId">The id of the cage</param>
		/// <returns>Returns list of AnimalCage</returns>
		Task<List<AnimalResponse>> GetAllAnimalInTheCage(int cageId);

		/// <summary>
		/// Get animal present cage
		/// </summary>
		/// <param name="animalId">The id of the animal</param>
		/// <returns>Returns the cage where the animal is in</returns>
		Task<CageResponse> GetAnimalPresentCage(long animalId);

		/// <summary>
		/// Move animal out of the cage by setting the IsIn property to false
		/// </summary>
		/// <param name="animalId">The id of an animal</param>
		/// <returns>Returns true if success, otherwise returns false</returns>
		Task<bool> MoveAnimalOut(long animalId);
	}
}
