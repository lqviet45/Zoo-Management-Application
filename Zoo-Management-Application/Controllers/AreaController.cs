using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AreasController : ControllerBase
	{
		// private field
		private readonly IAreaServices _areaServices;

		// constructor
		public AreasController(IAreaServices areaServices)
		{
			_areaServices = areaServices;
		}

		[HttpPost]
		public async Task<ActionResult<AreaResponse>> PostArea(AreaAddRequest areaAddRequest)
		{
			var areaResponse = await _areaServices.AddArea(areaAddRequest);

			return Ok(areaResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<AreaResponse>>> GetAllArea()
		{
			var listArea = await _areaServices.GetAllArea();
			return Ok(listArea);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AreaResponse>> GetAreaById(int? id)
		{
			var area = await _areaServices.GetAreaById(id);
			if (area == null) return NotFound();
			return Ok(area);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteArea(int? id)
		{
			var result = await _areaServices.DeleteArea(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AreaResponse>> UpdateArea(AreaUpdateRequest areaUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var areaUpdate = await _areaServices.UpdateArea(areaUpdateRequest);
				return Ok(areaUpdate);
			}

			return NotFound("Update was failed");
		}
	}
}
