using Entities.Models;
using System.Linq.Expressions;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing Animal entity
	/// </summary>
	public interface IAnimalRepositories
	{
		/// <summary>
		/// Adds a Animal object to the data store
		/// </summary>
		/// <param name="animal">The animal to add</param>
		/// <returns>Animal obj after adding</returns>
		Task<Animal> Add(Animal animal);

		/// <summary>
		/// Get all the animal  in the dataset
		/// </summary>
		/// <returns>A list of animal object</returns>
		Task<List<Animal>> GetAllAnimal();


		/// <summary>
		/// Get a animal by animal name
		/// </summary>
		/// <param name="name">The name of the animal</param>
		/// <returns>A matching animal object</returns>
		Task<Animal?> GetAnimalByName(string? name);

		/// <summary>
		/// Get a animal by animal Id
		/// </summary>
		/// <param name="animalId">The Id of the animal</param>
		/// <returns>A matching animal object</returns>
		Task<Animal?> GetAnimalById(long animalId);

		/// <summary>
		/// Get all animals by species Id
		/// </summary>
		/// <param name="animalId">The Id of the animal's species</param>
		/// <returns>List of matching animal object</returns>
		Task<List<Animal>> GetAnimalBySpeciesId(long speciesId);

		/// <summary>
		/// Detele an existed animal by id
		/// </summary>
		/// <param name="animalId">The id to delete</param>
		/// <returns>True if delete is success, else False</returns>
		Task<bool> DeleteAnimalById(long animalId);

		/// <summary>
		/// Update an existed animal
		/// </summary>
		/// <param name="animal">An animal to update</param>
		/// <returns>An animal after Updated</returns>
		Task<Animal> UpdateAnimal(Animal animal);

		/// <summary>
		/// Returns all animal objects that matches with the given
		/// </summary>
		/// <param name="predicate">Linq exoression to check</param>
		/// <returns>All matching Animal with given condition</returns>
		Task<List<Animal>> GetFilteredAnimal(Expression<Func<Animal, bool>> predicate);
	}
}
