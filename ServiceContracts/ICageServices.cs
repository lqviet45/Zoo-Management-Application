using ServiceContracts.DTO;
using ServiceContracts.DTO.AreaDTO;
using ServiceContracts.DTO.CageDTO;

namespace ServiceContracts
{
	public interface ICageServices
	{
		/// <summary>
		/// Adding new Cage into the Cage table
		/// </summary>
		/// <param name="cageAddRequest">The cage to add</param>
		/// <returns>CageResponse obj base on the cage adding </returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>"
		Task<CageResponse> AddCage(CageAddRequest? cageAddRequest);

		/// <summary>
		/// Get all the Cage in Cage table
		/// </summary>
		/// <returns>A list of Cage obj as CageResponse</returns>
		Task<List<CageResponse>> GetAllCage();

		/// <summary>
		/// Get Cage by Id 
		/// </summary>
		/// <param name="id">Cage ID</param>
		/// <returns>The mathcing Cage</returns>
		Task<CageResponse?> GetCageById(int? id);

		/// <summary>
		/// Deletes an Cage obj based on the given CageId
		/// </summary>
		/// <param name="id">Cage ID to delete</param>
		/// <returns>Returns true if the deletion is successful, otherwise false</returns>
		Task<bool> DeleteCage(int? id);

		/// <summary>
		/// Updates the specified Cage details based on the given cageId
		/// </summary>
		/// <param name="cageUpdateRequest">Cage detail to update including Cage ID</param>
		/// <returns>Returns the Cage response obj after updatation</returns>
		Task<CageResponse> UpdateCage(CageUpdateRequest? cageUpdateRequest);

		/// <summary>
		/// Get all cage that belong to an area
		/// </summary>
		/// <param name="areaId">The id of the area</param>
		/// <returns></returns>
		Task<List<CageResponse>> GetCageByAreaId(int areaId);

		/// <summary>
		/// Returns all cage objects that matches with the given search field and search string
		/// </summary>
		/// <param name="searchBy">The field to search</param>
		/// <param name="searchString">The string to search</param>
		/// <returns>Returns all the cage that matching base on the given field</returns>
		Task<List<CageResponse>> GetFilteredCage(string searchBy, string? searchString);
	}
}
