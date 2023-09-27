using Entities.Models;
using ServiceContracts.DTO.SkillDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.ExperienceDTO
{
	public class ExperienceUpdateRequest
	{
		[Required]
		public int ExperienceId { get; set; }

		[Required]
		public long UserId { get; set; }
		public List<SkillUpdateRequest> Skills { get; set; } = null!;

		public Experience MapToExperience()
		{
			return new Experience()
			{
				ExperienceId = ExperienceId,
				UserId = UserId,
				Skills = Skills.Select(s => s.MapToSkill()).ToList()
			};
		}
	}
}
