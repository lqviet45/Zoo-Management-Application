using ServiceContracts.DTO.CustommerDTO;

namespace ServiceContracts
{
	public interface ICustommerSevices
	{
		/// <summary>
		/// Add a custommer
		/// </summary>
		/// <param name="custommerAddRequest">The custommer add request</param>
		/// <returns>A custommer after Added</returns>
		Task<CustommerResponse> Add(CustommerAddRequest? custommerAddRequest);

		/// <summary>
		/// Get a List of Custommer by Email
		/// </summary>
		/// <param name="email">The email to Search</param>
		/// <returns>A list of matching custommer as CustommerResponse</returns>
		Task<List<CustommerResponse>> GetCustommerByEmail(string email);

		/// <summary>
		/// Get a custommer by Id
		/// </summary>
		/// <param name="custommerId">the custommer id to get</param>
		/// <returns>A matching custommer as CustommerResponse</returns>
		Task<CustommerResponse> GetCustommerById(long custommerId);
	}
}
