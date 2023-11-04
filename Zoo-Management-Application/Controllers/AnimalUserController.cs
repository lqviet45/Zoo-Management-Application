using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;
using ServiceContracts.DTO.UserDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "OfficeStaff,ZooTrainer")]
	public class AnimalUserController : ControllerBase
	{
		// private fields
		private readonly IAnimalUserServices _animalUserServices;
	

		// constructor
		public AnimalUserController(IAnimalUserServices animalUserServices)
		{
			_animalUserServices = animalUserServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<AnimalUserResponse>> PostAnimalUser(AnimalUserAddRequest animalUserAddRequest)
		{
			var animalUserResponse = await _animalUserServices.AddAnimalUser(animalUserAddRequest);

			return Ok(animalUserResponse);
		}

		[HttpGet("animal/{AnimalId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Animal>), Arguments = new object[] { "AnimalId", typeof(long) })]
		public async Task<ActionResult<List<UserResponse>>> GetZooTrainerByAnimalId(long AnimalId)
		{
			var animalUserResponse = await _animalUserServices.GetZooTrainerByAnimalId(AnimalId);

			if(animalUserResponse == null)
			{
				return NotFound("This Animal is not being trained by any Zoo Trainer!");
			}

			return Ok(animalUserResponse);
		}


		[HttpGet("user/{UserId}")]
		[Authorize(Roles = "ZooTrainer,OfficeStaff")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<User>), Arguments = new object[] { "UserId", typeof(long) })]
		public async Task<ActionResult<List<AnimalResponse>>> GetAnimalByZooTrainerId(long UserId)
		{
			var animalUserResponse = await _animalUserServices.GetAnimalByZooTrainerId(UserId);

			if (animalUserResponse == null)
			{
				return NotFound("This Zoo Trainer is not training any Animal!");
			}

			return Ok(animalUserResponse);
		}

		[HttpDelete("animal-trainer")]
		//[TypeFilter(typeof(ValidateEntityExistsAttribute<AnimalUser>), Arguments = new object[] { "AnimalUserId", typeof(long) })]
		public async Task<ActionResult<bool>> DeleteAnimalUser(long animalId, long userId)
		{
			var isDeleted = await _animalUserServices.DeleteAnimalUser(animalId, userId);

			if (!isDeleted)
			{
				return BadRequest("Delete Fails");
			}

			return Ok(isDeleted);
		}

		[HttpGet("animal-trainer-relationship")]
		[Authorize(Roles = "ZooTrainer,OfficeStaff")]
		public async Task<ActionResult<AnimalUserResponse>> GetAnimalUserRelationship(long animalId, long userId)
		{
			var animalUser = await _animalUserServices.GetAnimalUserRelationship(animalId, userId);
			if (animalUser == null) return NotFound();
			return Ok(animalUser);
			
		}
	}
}
