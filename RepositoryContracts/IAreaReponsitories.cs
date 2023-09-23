using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing Area entity
	/// </summary>
	public interface IAreaRepositories
	{
		/// <summary>
		/// Adds a Area object to the data store
		/// </summary>
		/// <param name="area">The area to add</param>
		/// <returns>User obj after adding</returns>
		Task<Area> Add(Area area);

		/// <summary>
		/// Get all the area in the dataset
		/// </summary>
		/// <returns>A list of Area obj</returns>
		Task<List<Area>> GetAllArea();

		/// <summary>
		/// Get area by id in the dataset
		/// </summary>
		/// <param name="areaId">The id of the area</param>
		/// <returns>Matched Area</returns>
		Task<Area?> GetAreaById(int? areaId);
	}
}
