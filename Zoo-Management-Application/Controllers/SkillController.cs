using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Zoo.Management.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SkillController : ControllerBase
	{
		private readonly ISkillServices _skillServices;
        public SkillController(ISkillServices skillServices)
        {
            _skillServices = skillServices;
        }

		[HttpDelete("{skillId}")]
		public async Task<IActionResult> DeleteSkill(int skillId)
		{
			var isDeleted = await _skillServices.DeleteSkill(skillId);
			if (!isDeleted) return NotFound("Delete Fail by some error!!");

			return Ok();
		}
    }
}
