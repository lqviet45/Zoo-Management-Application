using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AreaDTO;
using ServiceContracts.DTO.CageDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "OfficeStaff")]
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
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<CageResponse>> PostCage(CageAddRequest cageAddRequest)
		{
			var cageResponse = await _cageServices.AddCage(cageAddRequest);
			var CageId = new { CageId = cageResponse.CageId };

			return CreatedAtAction("GetCageById", CageId, cageResponse);
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

		[HttpGet("{CageId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Cage>), Arguments = new object[] { "CageId", typeof(int) })]
		public async Task<ActionResult<CageResponse>> GetCageById(int? CageId)
		{
			var cage = await _cageServices.GetCageById(CageId);
			if (cage == null) return NotFound();
			return Ok(cage);
		}

		[HttpGet("area/{AreaId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Area>), Arguments = new object[] { "AreaId", typeof(int) })]
		public async Task<ActionResult<List<CageResponse>>> GetCageByAreaId(int AreaId)
		{
			var listCage = await _cageServices.GetCageByAreaId(AreaId);
			if (listCage == null) return NotFound();
			return Ok(listCage);
		}

		[HttpDelete("{CageId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Cage>), Arguments = new object[] { "CageId", typeof(int) })]
		public async Task<ActionResult<bool>> DeleteCage(int? CageId)
		{
			var result = await _cageServices.DeleteCage(CageId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
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
