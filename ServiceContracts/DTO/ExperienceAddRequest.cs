using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class ExperienceAddRequest
	{
		[Required]
		public long UserId { get; set; }
		public List<SkillAddRequest> Skills { get; set; } = null!;

		public Experience MapToExperience()
		{
			return new Experience()
			{
				UserId = UserId,
				Skills = Skills.Select(s => s.MapToSkill()).ToList()
			};
		}
	}
}
