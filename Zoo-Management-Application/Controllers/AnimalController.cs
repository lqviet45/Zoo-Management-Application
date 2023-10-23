using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

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
		[Authorize(Roles ="OfficeStaff")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<AnimalResponse>> PostAnimal(AnimalAddRequest addrequest)
		{
			var animalResponse = await _animalServices.AddAnimal(addrequest);

			var AnimalId = new { AnimalId = animalResponse.AnimalId };

			return CreatedAtAction("GetAnimalById", AnimalId, animalResponse);
		}

		[HttpGet]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		public async Task<ActionResult<List<AnimalResponse>>> GetAllAnimal(int? pageNumber, string searchBy = "AnimalName", string? searchString = null)
		{
			var listAnimal = await _animalServices.GetFiteredAnimal(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<AnimalResponse>.CreateAsync(listAnimal.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{AnimalId}")]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Animal>), Arguments = new object[] { "AnimalId", typeof(long) })]
		public async Task<ActionResult<AnimalResponse>> GetAnimalById(long AnimalId)
		{
			var animal = await _animalServices.GetAnimalById(AnimalId);
			if (animal == null) return NotFound();
			return Ok(animal);
		}

		[HttpDelete("{AnimalId}")]
		[Authorize(Roles = "OfficeStaff")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Animal>), Arguments = new object[] { "AnimalId", typeof(long) })]
		public async Task<ActionResult<bool>> DeleteAnimal(long AnimalId)
		{
			var result = await _animalServices.DeleteAnimal(AnimalId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[Authorize(Roles = "OfficeStaff,ZooTrainner")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
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
