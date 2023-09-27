using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

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
		public async Task<ActionResult<SpeciesResponse>> PostSpecies(SpeciesAddRequest speciesAddRequest)
		{
			var speciesResponse = await _speciesServices.AddSpecies(speciesAddRequest);
			var id = new { id = speciesResponse.SpeciesId };

			return CreatedAtAction("GetSpeciesById", id, speciesResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<SpeciesResponse>>> GetAllSpecies()
		{
			var listSpecies = await _speciesServices.GetAllSpecies();
			return Ok(listSpecies);
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
		public async Task<ActionResult<SpeciesResponse>> UpdateSpecies(SpeciesUpdateRequest speciesUpdateRequest)
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
