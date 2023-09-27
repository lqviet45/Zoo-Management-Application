using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.ExperienceDTO;
using ServiceContracts.DTO.UserDTO;
using Services.Helper;
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

		public async Task<bool> DeleteExperience(int experienceId)
		{
			var experience = _experienceRepositories.GetExperienceById(experienceId);
			if (experience is null) return false;

			var isDelete = await _experienceRepositories.Delete(experienceId);
			return isDelete;
		}

		public async Task<ExperienceResponse> UpdateExperience(ExperienceUpdateRequest? experienceUpdateRequest)
		{
			if (experienceUpdateRequest == null) throw new ArgumentNullException("The update request is empty!");

			ValidationHelper.ModelValidation(experienceUpdateRequest);

			var experienceExist = await _experienceRepositories.GetExperienceById(experienceUpdateRequest.ExperienceId);

			if (experienceExist is null) throw new ArgumentException("The given experience id doesn't exist!");

			experienceExist.Skills = experienceUpdateRequest.Skills.Select(s => s.MapToSkill()).ToList();

			var experienceResponse = await _experienceRepositories.Update(experienceExist);

			return experienceResponse.ToExperienceResponse();
		}
	}
}
