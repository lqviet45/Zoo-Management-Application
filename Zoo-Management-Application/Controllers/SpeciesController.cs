using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.SpeciesDTO;
using ServiceContracts.DTO.WrapperDTO;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
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
		public async Task<ActionResult<SpeciesResponse>> PostSpecies([FromForm]SpeciesAddRequest speciesAddRequest)
		{
			var speciesResponse = await _speciesServices.AddSpecies(speciesAddRequest);
			var id = new { id = speciesResponse.SpeciesId };

			return CreatedAtAction("GetSpeciesById", id, speciesResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<SpeciesResponse>>> GetAllSpecies(int? pageNumber, string searchBy = "SpeciesName", string? searchString = null)
		{
			var listSpecies = await _speciesServices.GetFilteredSpecies(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<SpeciesResponse>.CreateAsync(listSpecies.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SpeciesResponse>> GetSpeciesById(int? id)
		{
			var species = await _speciesServices.GetSpeciesById(id);
			if (species == null) return NotFound();
			return Ok(species);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteSpecies(int? id)
		{
			var result = await _speciesServices.DeleteSpecies(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
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
