using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.SpeciesDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class SpeciesController : ControllerBase
	{
		// private field
		private readonly ISpeciesServices _speciesServices;

		// constructor
		public SpeciesController(ISpeciesServices speciesServices)
		{
			_speciesServices = speciesServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<SpeciesResponse>> PostSpecies([FromForm]SpeciesAddRequest speciesAddRequest)
		{
			var speciesResponse = await _speciesServices.AddSpecies(speciesAddRequest);
			var SpeciesId = new { SpeciesId = speciesResponse.SpeciesId };

			return CreatedAtAction("GetSpeciesById", SpeciesId, speciesResponse);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<List<SpeciesResponse>>> GetAllSpecies(int? pageNumber, string searchBy = "SpeciesName", string? searchString = null)
		{
			var listSpecies = await _speciesServices.GetFilteredSpecies(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<SpeciesResponse>.CreateAsync(listSpecies.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{SpeciesId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Species>), Arguments = new object[] { "SpeciesId", typeof(int) })]
		[AllowAnonymous]
		public async Task<ActionResult<SpeciesResponse>> GetSpeciesById(int? SpeciesId)
		{
			var species = await _speciesServices.GetSpeciesById(SpeciesId);
			if (species == null) return NotFound();
			return Ok(species);
		}

		[HttpDelete("{SpeciesId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Species>), Arguments = new object[] { "SpeciesId", typeof(int) })]
		public async Task<ActionResult<bool>> DeleteSpecies(int? SpeciesId)
		{
			var result = await _speciesServices.DeleteSpecies(SpeciesId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<SpeciesResponse>> UpdateSpecies([FromForm]SpeciesUpdateRequest speciesUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var speciesUpdate = await _speciesServices.UpdateSpecies(speciesUpdateRequest);
				return Ok(speciesUpdate);
			}

			return BadRequest();
		}
	}
}
