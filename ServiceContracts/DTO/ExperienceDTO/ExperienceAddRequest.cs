using Entities.Models;
using ServiceContracts.DTO.SkillDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.ExperienceDTO
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
