using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	public class ExperienceResponse
	{
		public int ExperienceId { get; set; }
		[Required]
		public int YearExp { get; set; }
		public List<Skill>? Skills { get; set; }

		public virtual User? User { get; set; }
	}

	public static class ExperienceExtension
	{
		public static ExperienceResponse ToExperienceResponse(this Experience experience)
		{
			return new ExperienceResponse()
			{
				ExperienceId = experience.ExperienceId,
				YearExp = experience.YearExp,
				Skills = experience.Skills?.ToList()
			};
		}
	}
}
