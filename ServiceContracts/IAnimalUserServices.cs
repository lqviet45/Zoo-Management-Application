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
	}
}
