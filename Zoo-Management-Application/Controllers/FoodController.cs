using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.FoodDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodController : ControllerBase
	{
		// private field
		private readonly IFoodServices _foodServices;

		// constructor
		public FoodController(IFoodServices foodServices)
		{
			_foodServices = foodServices;
		}

		[HttpPost]
		public async Task<ActionResult<FoodResponse>> PostFood(FoodAddRequest foodAddRequest)
		{
			var foodResponse = await _foodServices.AddFood(foodAddRequest);
			var id = new { id = foodResponse.FoodId };

			return CreatedAtAction("GetFoodById", id, foodResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<FoodResponse>>> GetAllFood()
		{
			var listFood = await _foodServices.GetAllFood();
			return Ok(listFood);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<FoodResponse>> GetFoodById(int id)
		{
			var food = await _foodServices.GetFoodById(id);
			if (food == null) return NotFound();
			return Ok(food);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteFood(int id)
		{
			var result = await _foodServices.DeleteFood(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<FoodResponse>> UpdateFood(FoodUpdateRequest foodUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var foodUpdate = await _foodServices.UpdateFood(foodUpdateRequest);
				return Ok(foodUpdate);
			}

			return NotFound("Can not update for some error");
		}
	}
}
