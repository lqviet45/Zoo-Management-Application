using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.SkillDTO;

namespace Services
{
	public class SkillServices : ISkillServices
	{
		private readonly ISkillRepositories _skillRepository;

        public SkillServices(ISkillRepositories skillRepositories)
        {
            _skillRepository = skillRepositories;
        }

        public async Task<List<SkillResponse>> AddSkills(List<SkillAddRequest> skills)
		{

			var listSkill = skills.Select(s => s.MapToSkill()).ToList();

			await _skillRepository.Add(listSkill);

			return listSkill.Select(s => s.ToSkillResponse()).ToList();
		}

		public async Task<bool> DeleteSkill(int skillId)
		{
			var skillExisted = await _skillRepository.GetBySkillId(skillId);

			if (skillExisted is null)
			{
				return false;
			}

			var isDeleted = await _skillRepository.Delete(skillExisted.SkillId);

			return isDeleted;
		}

		public async Task<List<SkillResponse>> GetSkillByUserId(long userId)
		{
			var listSkill = await _skillRepository.GetByUserId(userId);

			return listSkill.Select(s => s.ToSkillResponse()).ToList();
		}

		public async Task<List<SkillResponse>> UpdateSkills(List<SkillUpdateRequest> skills)
		{
			var listSkill = skills.Select(s => s.MapToSkill()).Where(s => s.SkillId != 0).ToList();

			var listSkillAdd = skills.Select(s => s.MapToSkill()).Where(s => s.SkillId == 0).ToList();

			foreach (var skill in listSkill)
			{
				await _skillRepository.Update(skill);
			}
			
			if (listSkillAdd.Count > 0)
			{
				await _skillRepository.Add(listSkillAdd);
				listSkill.AddRange(listSkillAdd);
			}

			return listSkill.Select(s => s.ToSkillResponse()).ToList();
		}
	}
}
