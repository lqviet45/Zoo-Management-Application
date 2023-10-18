using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.AreaDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

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
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<AreaResponse>> PostArea(AreaAddRequest areaAddRequest)
		{
			var areaResponse = await _areaServices.AddArea(areaAddRequest);
			var id = new { id = areaResponse.AreaId };

			return CreatedAtAction("GetAreaById", id, areaResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<AreaResponse>>> GetAllArea(int? pageNumber, string searchBy = "AreaName", string? searchString = null)
		{
			var listArea = await _areaServices.GetFilteredArea(searchBy, searchString);

			int pageSize = 5;

			var pagingList = PaginatedList<AreaResponse>.CreateAsync(listArea.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);

			var response = new { pagingList, pagingList.TotalPages };

			return Ok(response);
		}

		[HttpGet("{AreaId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Area>), Arguments = new object[] { "AreaId", typeof(int) })]
		public async Task<ActionResult<AreaResponse>> GetAreaById(int? AreaId)
		{
			var area = await _areaServices.GetAreaById(AreaId);
			if (area == null) return NotFound();
			return Ok(area);
		}


		[HttpDelete("{AreaId}")]
		public async Task<ActionResult<bool>> DeleteArea(int? id)
		{
			var result = await _areaServices.DeleteArea(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
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
