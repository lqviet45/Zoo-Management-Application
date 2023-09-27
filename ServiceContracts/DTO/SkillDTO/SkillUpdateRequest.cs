using Entities.Models;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO.SkillDTO
{
	public class SkillUpdateRequest
	{
		[Required]
		public int SkillId { get; set; }

		[Required]
		[StringLength(40)]
		public string? SkillName { get; set; }

		public Skill MapToSkill()
		{
			return new Skill()
			{
				SkillId = SkillId,
				SkillName = SkillName
			};
		}
	}
}
