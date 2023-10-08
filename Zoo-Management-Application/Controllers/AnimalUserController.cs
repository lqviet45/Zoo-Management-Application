using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AnimalUserDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnimalUserController : ControllerBase
	{
		// private fields
		private readonly IAnimalUserServices _animalUserServices;
		private readonly IAnimalServices _animalServices;
		private readonly IUserServices _userServices;

		// constructor
		public AnimalUserController(IAnimalUserServices animalUserServices, IAnimalServices animalServices, IUserServices userServices)
		{
			_animalUserServices = animalUserServices;
			_animalServices = animalServices;
			_userServices = userServices;
		}

		[HttpPost]
		public async Task<ActionResult<AnimalUserResponse>> PostAnimalUser(AnimalUserAddRequest animalUserAddRequest)
		{
			var animalUserResponse = await _animalUserServices.AddAnimalUser(animalUserAddRequest);

			return Ok(animalUserResponse);
		}

		[HttpGet("animal/{animalId}")]
		public async Task<ActionResult<AnimalUserResponse>> GetZooTrainerByAnimalId(long animalId)
		{
			var animalUserResponse = await _animalUserServices.GetZooTrainerByAnimalId(animalId);

			if(animalUserResponse == null)
			{
				return NotFound("This Animal is not being trained by any Zoo Trainer!");
			}

			return Ok(animalUserResponse);
		}


		[HttpGet("user/{userId}")]
		public async Task<ActionResult<AnimalUserResponse>> GetAnimalByZooTrainerId(long userId)
		{
			var animalUserResponse = await _animalUserServices.GetAnimalByZooTrainerId(userId);

			if (animalUserResponse == null)
			{
				return NotFound("This Zoo Trainer is not training any Animal!");
			}

			return Ok(animalUserResponse);
		}

		[HttpDelete("{animalId}/{userId}")]
		public async Task<ActionResult<bool>> DeleteAnimalUser(long animalId, long userId)
		{
			var isDeleted = await _animalUserServices.DeleteAnimalUser(animalId, userId);

			if (!isDeleted)
			{
				return BadRequest("This Animal is not being trained by this Zoo Trainer!");
			}

			return Ok(isDeleted);
		}

	}
}
