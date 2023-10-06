using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.MealDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MealController : ControllerBase
	{
		private readonly IMealServices _mealServices;

		public MealController(IMealServices mealServices)
		{
			_mealServices = mealServices;
		}

		[HttpPost]
		public async Task<ActionResult<MealResponse>> PostMeal(List<MealAddRequest> mealAddRequest)
		{
			

			var mealResponse = await _mealServices.AddMeal(mealAddRequest);

			var routeValues = new { Id = mealResponse.AnimalId };
			return CreatedAtAction("GetAnimalMealById", routeValues, mealResponse);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<List<MealResponse>>> GetAnimalMealById(long id)
		{
			var mealResponse = await _mealServices.GetAnimalMealById(id);
			return Ok(mealResponse);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteAMeal(MealDeleteRequest mealDeleteRequest)
		{
			var isDeleted = await _mealServices.DeleteAMeal(mealDeleteRequest);

			if (!isDeleted) return NotFound("The given animal id or feeding time doesn't exist!");

			return Ok(isDeleted);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteAFoodInAMeal(MealDeleteRequest2 deleteFood)
		{
			var isDeleted = await _mealServices.DeleteAFoodInAMeal(deleteFood);

			if (!isDeleted) return NotFound("The given animal id or feeding time or food id doesn't exist!");

			return Ok(isDeleted);
		}

		[HttpGet]
		public async Task<ActionResult<List<MealResponse>>> GetMealAtATime(long animalId, TimeSpan feedingTime)
		{
			var mealResponse = await _mealServices.GetAnimalMealByIdAndTime(animalId, feedingTime);
			
			return Ok(mealResponse);
		}
	}
}
