using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.FoodDTO;
using ServiceContracts.DTO.MealDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "ZooTrainer")]
	public class MealController : ControllerBase
	{
		private readonly IMealServices _mealServices;

		public MealController(IMealServices mealServices)
		{
			_mealServices = mealServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<AnimalFoodResponse>> PostMeal(List<MealAddRequest> mealAddRequest)
		{
			
			var mealResponse = await _mealServices.AddMeal(mealAddRequest);

			var AnimalUserId = new { AnimalUserId = mealResponse.AnimalUserId };

			return CreatedAtAction("GetAnimalMealById", AnimalUserId, mealResponse);
		}

		[HttpGet("{AnimalUserId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<AnimalFood>), Arguments = new object[] { "AnimalUserId", typeof(long) })]
		public async Task<ActionResult<List<MealResponse>>> GetAnimalMealById(long AnimalUserId)
		{
			var mealResponse = await _mealServices.GetAnimalMealById(AnimalUserId);

			return Ok(mealResponse);
		}

		[HttpDelete("AllMeal")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<bool>> DeleteAMeal(MealDeleteRequest mealDeleteRequest)
		{
			var isDeleted = await _mealServices.DeleteAMeal(mealDeleteRequest);

			if (!isDeleted) return NotFound("The given animal AnimalUserId or feeding time doesn't exist!");

			return Ok(isDeleted);
		}

		[HttpDelete("Meal")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<bool>> DeleteAFoodInAMeal(MealDeleteRequest2 deleteFood)
		{
			var isDeleted = await _mealServices.DeleteAFoodInAMeal(deleteFood);

			if (!isDeleted) return NotFound("The given animal AnimalUserId or feeding time or food AnimalUserId doesn't exist!");

			return Ok(isDeleted);
		}

		[HttpGet]
		public async Task<ActionResult<List<FoodResponse>>> GetMealAtATime(long AnimalUserId, TimeSpan FeedingTime)
		{
			var mealResponse = await _mealServices.GetAnimalMealByIdAndTime(AnimalUserId, FeedingTime);
			
			return Ok(mealResponse);
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<MealResponse>>> GetAllMeal()
		{
			var mealResponse = await _mealServices.GetAllMeal();

			return Ok(mealResponse);
		}

	}
}
