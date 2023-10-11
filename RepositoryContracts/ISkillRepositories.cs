using Entities.Models;

namespace RepositoryContracts
{
	public interface ISkillRepositories
	{
		/// <summary>
		/// Adding a list of skill to the database
		/// </summary>
		/// <param name="skills">The list skill to add</param>
		/// <returns>A list of skill after adding</returns>
		Task<List<Skill>> Add(List<Skill> skills);

		/// <summary>
		/// Update a exist skill in the database
		/// </summary>
		/// <param name="skills">The list skill to update</param>
		/// <returns>A list of skill after update</returns>
		Task<Skill> Update(Skill skills);

		/// <summary>
		/// Deleta a skill by skill id
		/// </summary>
		/// <param name="skillId">The skill Id to delete</param>
		/// <returns>True if delete success, else false</returns>
		Task<bool> Delete(int skillId);

		/// <summary>
		/// Get the skill by userId
		/// </summary>
		/// <param name="userId">The userId to get</param>
		/// <returns>A list of matching skill</returns>
		Task<List<Skill>> GetByUserId(long userId);

		/// <summary>
		/// Get the skill by skillId
		/// </summary>
		/// <param name="skillId">The skillId to get</param>
		/// <returns>A matching skill or null</returns>
		Task<Skill?> GetBySkillId(int skillId);
	}
}
