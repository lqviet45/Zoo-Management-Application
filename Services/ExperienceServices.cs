using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ExperienceServices : IExperienceServices
	{
		private readonly IExperienceRepositories _experienceRepositories;

		public ExperienceServices(IExperienceRepositories experienceRepositories)
		{
			_experienceRepositories = experienceRepositories;
		}

		public async Task<ExperienceResponse> AddExperience(ExperienceAddRequest? experienceAddRequest)
		{
			if (experienceAddRequest == null) throw new ArgumentNullException("Experiences is null!!");
			var experience = experienceAddRequest.MapToExperience();
			await _experienceRepositories.Add(experience);

			return experience.ToExperienceResponse();
		}

		public async Task<List<ExperienceResponse>> GetExperienceByUserId(long userId)
		{
			var listExperiences = await _experienceRepositories.GetExperienceByUserId(userId);
			var listExperienceResponse = listExperiences.Select(x => x.ToExperienceResponse()).ToList();

			return listExperienceResponse;
		}
	}
}
