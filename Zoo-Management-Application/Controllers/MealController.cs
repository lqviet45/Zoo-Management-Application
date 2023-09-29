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
		public async Task<ActionResult<MealResponse>> PostMeal(MealAddRequest mealAddRequest)
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
	}
}
