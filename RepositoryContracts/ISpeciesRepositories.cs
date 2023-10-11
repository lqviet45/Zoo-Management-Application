using Entities.Models;
using System.Linq.Expressions;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing Species entity
	/// </summary>
	public interface ISpeciesRepositories
	{
		/// <summary>
		/// Adds a Species object to the data store
		/// </summary>
		/// <param name="species">The species to add</param>
		/// <returns></returns>
		Task<Species> Add(Species species);

		/// <summary>
		/// Get all the species in the dataset
		/// </summary>
		/// <returns>A list of Species obj</returns>
		Task<List<Species>> GetAllSpecies();

		/// <summary>
		/// Get a species by Id
		/// </summary>
		/// <param name="speciesId">The ID of the species</param>
		/// <returns>The matching species</returns>
		Task<Species?> GetSpeciesById(int speciesId);

		/// <summary>
		/// Get species by name
		/// </summary>
		/// <param name="speciesName">The name of an species</param>
		/// <returns>The matching species</returns>
		Task<Species?> GetSpeciesByName(string speciesName);

		/// <summary>
		/// Update an existed species
		/// </summary>
		/// <param name="species">The ID of the species to update</param>
		/// <returns>A species after updated</returns>
		Task<Species> Update(Species species);

		/// <summary>
		/// Delete species by Id
		/// </summary>
		/// <param name="speciesId">Species ID to delete</param>
		/// <returns>Return true if delete successful, otherwise retruns false</returns>
		Task<bool> Delete(int speciesId);

		/// <summary>
		/// Returns all species objects that matches with the given
		/// </summary>
		/// <param name="predicate">Linq expression to check</param>
		/// <returns>All matching species with the given condition</returns>
		Task<List<Species>> GetFilteredSpecies(Expression<Func<Species, bool>> predicate);
	}
}
