using Entities.Models;

namespace RepositoryContracts
{
	public interface ICustommerReponsitories
	{
		/// <summary>
		/// Add a new custommer
		/// </summary>
		/// <param name="custommer">Custommer to add</param>
		/// <returns>A custommer after added</returns>
		Task<Custommer> AddCustommer(Custommer custommer);

		/// <summary>
		/// Get list of custommer by email
		/// </summary>
		/// <param name="email">a email to get</param>
		/// <returns>A list of matching custommer</returns>
		Task<List<Custommer>> GetCustommerByEmail(string email);

		/// <summary>
		/// Get A custommer by Id
		/// </summary>
		/// <param name="id">The custommer id to get</param>
		/// <returns>A matching custommer</returns>
		Task<Custommer?> GetCustommerById(long id);
	}
}
