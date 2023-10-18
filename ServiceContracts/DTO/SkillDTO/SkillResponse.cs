using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.SkillDTO
{
    public class SkillResponse
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
    }

    public static class SkillExtension
    {
        public static SkillResponse ToSkillResponse(this Skill skill)
        {
            return new SkillResponse()
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName
            };
        }
    }
}
