using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class ExperienceAddRequest
	{
		[Required]
		public long UserId { get; set; }
		[Required]
		public int ExperienceId { get; set; }
		[Required]
		public int YearExp { get; set; }
		public List<Skill>? Skills { get; set; }

		public Experience MapToExperience()
		{
			return new Experience()
			{
				ExperienceId = ExperienceId,
				YearExp = YearExp,
				Skills = Skills
			};
		}
	}
}
