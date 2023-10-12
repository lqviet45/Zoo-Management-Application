using Entities.Models;
using System.Linq.Expressions;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing Cage entity
	/// </summary>
	public interface ICageRepositories
	{
		/// <summary>
		/// Adds a Area object to the data store
		/// </summary>
		/// <param name="cage">The cage to add</param>
		/// <returns>Cage obj after adding</returns>
		Task<Cage> Add(Cage cage);

		/// <summary>
		/// Get all the cages in the dataset
		/// </summary>
		/// <returns>A list of Cage obj</returns>
		Task<List<Cage>> GetAllCage();

		/// <summary>
		/// Get cage by id in the dataset
		/// </summary>
		/// <param name="cageId">The id of the cage</param>
		/// <returns>Matched Cage</returns>
		Task<Cage?> GetCageById(int? cageId);

		/// <summary>
		/// Get Cage by name in the dataset
		/// </summary>
		/// <param name="CageName">a character or a name </param>
		/// <returns>Matching Area </returns>
		Task<Cage?> GetCageByName(string cageName);

		/// <summary>
		/// Updates a Cage obj based on the given CageId 
		/// </summary>
		/// <param name="cage">Cage obj to update </param>
		/// <returns>Returns the updated Cage object</returns>
		Task<Cage> UpdateCage(Cage cage);

		/// <summary>
		/// Deletes a Cage obj based on the given CageId
		/// </summary>
		/// <param name="cageId">Cage ID to search</param>
		/// <returns>Returns true if the deletion is successful otherwise false</returns>
		Task<bool> DeleteCage(int AreaId);

		/// <summary>
		/// Get all Cage that have the same AreaId
		/// </summary>
		/// <param name="areaId">The area id</param>
		/// <returns>Returns all matching cage </returns>
		Task<List<Cage>> GetCageByAreaId(int areaId);

		/// <summary>
		/// Returns all Cage object base on the given expression
		/// </summary>
		/// <param name="predicate">Linq expression to check</param>
		/// <returns>All areas that matching with given condition </returns>
		Task<List<Cage>> GetFilteredCage(Expression<Func<Cage, bool>> predicate);
	}
}
