using ServiceContracts.DTO.SkillDTO;

namespace ServiceContracts
{
	public interface ISkillServices
	{
		/// <summary>
		/// Adding a list of skill
		/// </summary>
		/// <param name="skills">The list of skillAddRequest</param>
		/// <returns>A list of skill after adding as skillResponse type</returns>
		Task<List<SkillResponse>> AddSkills(List<SkillAddRequest> skills);

		/// <summary>
		/// Update a list of skill
		/// </summary>
		/// <param name="skills">The list skill to update</param>
		/// <returns>A list of skill after update</returns>
		Task<List<SkillResponse>> UpdateSkills(List<SkillUpdateRequest> skills);

		/// <summary>
		/// Remove a existing skill by id
		/// </summary>
		/// <param name="skillId">The skill Id to remove</param>
		/// <returns>True if success, else false</returns>
		Task<bool> DeleteSkill(int skillId);

		/// <summary>
		/// Get Skills by userId
		/// </summary>
		/// <param name="userId">The userId to get</param>
		/// <returns>A list of matching skill</returns>
		Task<List<SkillResponse>> GetSkillByUserId(long userId);
	}
}
