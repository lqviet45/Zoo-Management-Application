using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AnimalAddDTO;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;

namespace Zoo_Management_Application.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AnimalController : ControllerBase
	{
		// private field
		private readonly IAnimalServices _animalServices;
		private readonly IAnimalUserServices _animalUserServices;

		// constructor
		public AnimalController(IAnimalServices animalServices, IAnimalUserServices animalUserServices)
		{
			_animalServices = animalServices;
			_animalUserServices = animalUserServices;
		}

		[HttpPost]
		public async Task<ActionResult<AnimalResponse>> PostAnimal(AnimalAdd animaladd)
		{
			var animalResponse = await _animalServices.AddAnimal(animaladd);

			var id = new { id = animalResponse.AnimalId };

			return CreatedAtAction("GetAnimalById", id, animalResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<AnimalResponse>>> GetAllAnimal()
		{
			var listAnimal = await _animalServices.GetAnimalList();
			return Ok(listAnimal);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AnimalResponse>> GetAnimalById(int id)
		{
			var animal = await _animalServices.GetAnimalById(id);
			if (animal == null) return NotFound();
			return Ok(animal);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteAnimal(int id)
		{
			var result = await _animalServices.DeleteAnimal(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<AnimalResponse>> UpdateAnimal(AnimalUpdateRequest animalUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var animal = await _animalServices.UpdateAnimal(animalUpdateRequest);
				if (animal == null) return NotFound();
				return Ok(animal);
			}
			return BadRequest();
		}

	}
}
