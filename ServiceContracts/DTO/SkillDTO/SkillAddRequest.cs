using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.SkillDTO
{
    public class SkillAddRequest
    {
        [Required]
        [StringLength(40)]
        public string? SkillName { get; set; }

        [Required]
        public int ExperienceId { get; set; }

        public Skill MapToSkill()
        {
            return new Skill()
            {
                SkillName = SkillName,
                ExperienceId = ExperienceId
            };
        }
    }
}
