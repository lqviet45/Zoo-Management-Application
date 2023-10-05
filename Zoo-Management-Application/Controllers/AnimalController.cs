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
	}
}
