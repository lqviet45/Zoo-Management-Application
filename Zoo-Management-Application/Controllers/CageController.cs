using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AreaDTO;
using ServiceContracts.DTO.CageDTO;
using ServiceContracts.DTO.WrapperDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CageController : ControllerBase
	{
		// private field
		private readonly ICageServices _cageServices;

		// constructor
		public CageController(ICageServices cageServices)
		{
			_cageServices = cageServices;
		}

		[HttpPost]
		public async Task<ActionResult<CageResponse>> PostCage(CageAddRequest cageAddRequest)
		{
			var cageResponse = await _cageServices.AddCage(cageAddRequest);
			var id = new { id = cageResponse.CageId };

			return CreatedAtAction("GetCageById", id, cageResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<CageResponse>>> GetAllCage(int? pageNumber, string searchBy = "CageName", string? searchString = null)
		{
			var listCage = await _cageServices.GetFilteredCage(searchBy, searchString);

			int pageSize = 5;

			var pagingList = PaginatedList<CageResponse>.CreateAsync(listCage.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);

			var response = new { pagingList, pagingList.TotalPages };

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CageResponse>> GetCageById(int? id)
		{
			var cage = await _cageServices.GetCageById(id);
			if (cage == null) return NotFound();
			return Ok(cage);
		}

		[HttpGet("area/{areaId}")]
		public async Task<ActionResult<List<CageResponse>>> GetCageByAreaId(int areaId)
		{
			var listCage = await _cageServices.GetCageByAreaId(areaId);
			if (listCage == null) return NotFound();
			return Ok(listCage);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteCage(int? id)
		{
			var result = await _cageServices.DeleteCage(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<CageResponse>> UpdateCage(CageUpdateRequest cageUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var cageUpdate = await _cageServices.UpdateCage(cageUpdateRequest);
				return Ok(cageUpdate);
			}
			return NotFound("Update was failed"); ;
		}

	}
}
