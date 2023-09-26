using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	public interface IExperienceServices
	{
		/// <summary>
		/// Add Experience to database
		/// </summary>
		/// <param name="experienceAddRequest">The experience request to add</param>
		/// <returns>A Experience object as ExperienceResponse</returns>
		Task<ExperienceResponse> AddExperience(ExperienceAddRequest? experienceAddRequest);

		/// <summary>
		/// Get Experiences by user id
		/// </summary>
		/// <param name="userId">the user id to get</param>
		/// <returns>A list of Experiences as ExperienceResponse</returns>
		Task<List<ExperienceResponse>> GetExperienceByUserId(long userId);

		/// <summary>
		/// Delete a Experience by Id
		/// </summary>
		/// <param name="experienceId">The experience id to delete</param>
		/// <returns>True if delete success, else false</returns>
		Task<bool> DeleteExperience(int experienceId);
	}
}
