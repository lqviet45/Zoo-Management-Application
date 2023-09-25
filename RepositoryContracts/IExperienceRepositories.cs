using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
	public interface IExperienceRepositories
	{
		/// <summary>
		/// Adding a Experience to database
		/// </summary>
		/// <param name="experience">The experience to add</param>
		/// <returns>A Expericece object</returns>
		Task<Experience> Add(Experience experience);

		/// <summary>
		/// Get the Experiences By userId
		/// </summary>
		/// <param name="userId">The user Id to get</param>
		/// <returns>List of Experiences</returns>
		Task<List<Experience>> GetExperienceByUserId(long userId);
	}
}
