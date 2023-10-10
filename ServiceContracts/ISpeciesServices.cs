using ServiceContracts.DTO.SpeciesDTO;

namespace ServiceContracts
{
    public interface ISpeciesServices
	{
		/// <summary>
		/// Adding the new Species into the Species table
		/// </summary>
		/// <param name="areaAddRequest">The Species to add</param>
		/// <returns>SpeciesResponse obj base on the Species adding</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>"
		Task<SpeciesResponse> AddSpecies(SpeciesAddRequest? speciesAddRequest);

		/// <summary>
		/// Get All the Species in Species table
		/// </summary>
		/// <returns>A list of Species obj as SpeciesResponse</returns>
		Task<List<SpeciesResponse>> GetAllSpecies();

		/// <summary>
		/// Get Species by Id
		/// </summary>
		/// <param name="id">Species ID</param>
		/// <returns>The matching Species</returns>
		Task<SpeciesResponse?> GetSpeciesById(int? id);

		/// <summary>
		/// Deletes an Species obj based on the given SpeciesId
		/// </summary>
		/// <param name="id">SpeciesId to delete</param>
		/// <returns>Returns true if the deletion is successful, otherwise false</returns>
		Task<bool> DeleteSpecies(int? id);

		/// <summary>
		/// Updates the specified Species details based on the given SpeciesId
		/// </summary>
		/// <param name="areaUpdateRequest">Species details to update, including Species ID</param>
		/// <returns>Returns the Species response obj after updation</returns>
		Task<SpeciesResponse> UpdateSpecies(SpeciesUpdateRequest? speciesUpdateRequest);

		/// <summary>
		/// Returns all species obj that matches with the given search field and search string
		/// </summary>
		/// <param name="searchBy">The field to search</param>
		/// <param name="searchString">The string to search</param>
		/// <returns>Returns all matching species base on the given search field and search string</returns>
		Task<List<SpeciesResponse>> GetFilteredSpecies(string searchBy, string? searchString);
	}
}
