using Entities.Models;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
	public class ExperienceResponse
	{
		public int ExperienceId { get; set; }
		[Required]
		public List<SkillResponse> Skills { get; set; } = null!;
	}

	public static class ExperienceExtension
	{
		public static ExperienceResponse ToExperienceResponse(this Experience experience)
		{
			return new ExperienceResponse()
			{
				ExperienceId = experience.ExperienceId,
				Skills = experience.Skills.Select(s => s.ToSkillResponse()).ToList()
			};
		}
	}
}
