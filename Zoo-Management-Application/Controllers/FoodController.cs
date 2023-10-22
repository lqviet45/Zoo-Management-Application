using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.FoodDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "OfficeStaff")]
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
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<FoodResponse>> PostFood(FoodAddRequest foodAddRequest)
		{
			var foodResponse = await _foodServices.AddFood(foodAddRequest);
			var FoodId = new { FoodId = foodResponse.FoodId };

			return CreatedAtAction("GetFoodById", FoodId, foodResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<FoodResponse>>> GetAllFood(int? pageNumber, string searchBy = "FoodName", string? searchString = null)
		{
			var listFood = await _foodServices.GetFilteredFood(searchBy, searchString);
			int pageSize = 5;
			var pagingList = PaginatedList<FoodResponse>.CreateAsync(listFood.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{FoodId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Food>), Arguments = new object[] { "FoodId", typeof(int) })]
		public async Task<ActionResult<FoodResponse>> GetFoodById(int FoodId)
		{
			var food = await _foodServices.GetFoodById(FoodId);
			if (food == null) return NotFound();
			return Ok(food);
		}

		[HttpDelete("{FoodId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Food>), Arguments = new object[] { "FoodId", typeof(int) })]
		public async Task<ActionResult<bool>> DeleteFood(int FoodId)
		{
			var result = await _foodServices.DeleteFood(FoodId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<FoodResponse>> UpdateFood(FoodUpdateRequest foodUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var foodUpdate = await _foodServices.UpdateFood(foodUpdateRequest);
				return Ok(foodUpdate);
			}

			return BadRequest("Can not update for some error");
		}
	}
}
