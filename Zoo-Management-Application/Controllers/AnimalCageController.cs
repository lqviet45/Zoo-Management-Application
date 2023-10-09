using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AnimalCageDTO;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.CageDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnimalCageController : ControllerBase
	{
		// private fields
		private readonly IAnimalCageServices _animalCageServices;
		private readonly IAnimalServices _animalServices;
		private readonly ICageServices _cageServices;

		// constructor
		public AnimalCageController(IAnimalCageServices animalCageServices, IAnimalServices animalServices, ICageServices cageServices)
		{
			_animalCageServices = animalCageServices;
			_animalServices = animalServices;
			_cageServices = cageServices;
		}

		[HttpPost]
		public async Task<ActionResult<AnimalCageResponse>> PostAnimalCage(AnimalCageAddRequest animalCageAddRequest)
		{
			var animalCageResponse = await _animalCageServices.Add(animalCageAddRequest);

			return Ok(animalCageResponse);
		}


		[HttpGet("animal/{animalId}")]
		public async Task<ActionResult<List<CageResponse>>> GetAnimalCageHistory(long animalId)
		{
			var cageResponse = await _animalCageServices.GetAnimalCageHistory(animalId);

			if (cageResponse == null)
			{
				return NotFound("This Animal has not been in any Cage!");
			}

			return Ok(cageResponse);
		}

		[HttpGet("cage/{cageId}")]
		public async Task<ActionResult<List<AnimalResponse>>> GetAnimalInTheCage(int cageId)
		{
			var animalResponse = await _animalCageServices.GetAllAnimalInTheCage(cageId);

			if (animalResponse == null)
			{
				return NotFound("This Cage has no Animal!");
			}

			return Ok(animalResponse);
		}


		[HttpGet("present/{animalId}")]
		public async Task<ActionResult<CageResponse>> GetAnimalPresentCage(long animalId)
		{
			var cageResponse = await _animalCageServices.GetAnimalPresentCage(animalId);

			if (cageResponse == null)
			{
				return NotFound("This Animal is not in any Cage!");
			}

			return Ok(cageResponse);
		}

	}
}
