using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AnimalCageDTO;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.CageDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnimalCageController : ControllerBase
	{
		// private fields
		private readonly IAnimalCageServices _animalCageServices;

		// constructor
		public AnimalCageController(IAnimalCageServices animalCageServices)
		{
			_animalCageServices = animalCageServices;
		}

		[HttpPut("move-animal")]
		[Authorize(Roles = "OfficeStaff")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		// Move Animal to Another Cage
		public async Task<ActionResult<AnimalCageResponse>> MoveAnimalToOtherCage(AnimalCageUpdateRequest animalCageUpdateRequest)
		{
			var animalCageResponse = await _animalCageServices.UpdateAnimalCage(animalCageUpdateRequest);

			return Ok(animalCageResponse);
		}


		[HttpGet("animal/{AnimalId}")]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Animal>), Arguments = new object[] { "AnimalId", typeof(long) })]
		public async Task<ActionResult<List<CageResponse>>> GetAnimalCageHistory(long AnimalId)
		{
			var cageResponse = await _animalCageServices.GetAnimalCageHistory(AnimalId);

			if (cageResponse == null)
			{
				return NotFound("This Animal has not been in any Cage!");
			}

			return Ok(cageResponse);
		}

		[HttpGet("cage/{CageId}")]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Cage>), Arguments = new object[] { "CageId", typeof(int) })]
		public async Task<ActionResult<List<AnimalResponse>>> GetAnimalInTheCage(int CageId)
		{
			var animalResponse = await _animalCageServices.GetAllAnimalInTheCage(CageId);

			if (animalResponse == null)
			{
				return NotFound("This Cage has no Animal!");
			}

			return Ok(animalResponse);
		}


		[HttpGet("present/{AnimalId}")]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Animal>), Arguments = new object[] { "AnimalId", typeof(long) })]
		public async Task<ActionResult<CageResponse>> GetAnimalPresentCage(long AnimalId)
		{
			var cageResponse = await _animalCageServices.GetAnimalPresentCage(AnimalId);

			if (cageResponse == null)
			{
				return NotFound("This Animal is not in any Cage!");
			}

			return Ok(cageResponse);
		}

	}
}
