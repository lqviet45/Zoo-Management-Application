using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class ExperienceAddRequest
	{
		[Required]
		public long UserId { get; set; }
		public List<Skill> Skills { get; set; } = null!;

		public Experience MapToExperience()
		{
			return new Experience()
			{
				UserId = UserId,
				Skills = Skills
			};
		}
	}
}
