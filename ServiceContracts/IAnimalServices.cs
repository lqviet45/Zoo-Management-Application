using ServiceContracts.DTO.AnimalDTO;


namespace ServiceContracts
{
	public interface IAnimalServices
	{
		/// <summary>
		/// Adding the new Animal into the Animal table
		/// </summary>
		/// <param name="animalAdd">The animal to add</param>
		/// <returns>AnimalResponse object base on the animal adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<AnimalResponse> AddAnimal(AnimalAddRequest animalAdd);

		/// <summary>
		/// Get All the Animal having in Animal table
		/// </summary>
		/// <returns>A list of Animal object as AnimalResponse</returns>
		Task<List<AnimalResponse>> GetAnimalList();

		/// <summary>
		/// Get an animal by Id
		/// </summary>
		/// <param name="animalId">The animal Id to get</param>
		/// <returns>Matching user object as AnimalResponse type</returns>
		Task<AnimalResponse?> GetAnimalById(long animalId);

		/// <summary>
		/// Delete animal by id
		/// </summary>
		/// <param name="animalId">The animal id to delete</param>
		/// <returns>True if delete success, else False</returns>
		Task<bool> DeleteAnimal(long animalId);


		/// <summary>
		/// Updates the specified animal details based on the given animal ID
		/// </summary>
		/// <param name="animalUpdateRequest">Animal details to update</param>
		/// <returns>Returns the animal response object updated</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		Task<AnimalResponse> UpdateAnimal(AnimalUpdateRequest animalUpdateRequest);

		/// <summary>
		/// Returns all Animal objects that matches with the given search field and search string
		/// </summary>
		/// <param name="searchBy">Search field to search</param>
		/// <param name="searchString">Search string to search</param>
		/// <returns>Returns all matching Animal base on the given search field and search string</returns>
		Task<List<AnimalResponse>> GetFiteredAnimal(string searchBy, string? searchString);
 	}
}
