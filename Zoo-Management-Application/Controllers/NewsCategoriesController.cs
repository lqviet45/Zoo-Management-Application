using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO. NewsCategoriesDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsCategoriesController : ControllerBase
	{
		// private field
		private readonly INewsCategoriesServices _newsCategoryServices;

		// constructor
		public NewsCategoriesController(INewsCategoriesServices newsCategoryServices)
		{
			_newsCategoryServices = newsCategoryServices;
		}

		[HttpPost]
		public async Task<ActionResult<NewsCategoryResponse>> PostNewsCategory(NewsCategoryAddRequest newsCategoryAddRequest)
		{
			var newsCategoryResponse = await _newsCategoryServices.AddNewsCategories(newsCategoryAddRequest);
			var id = new { id = newsCategoryResponse.CategoryId };

			return CreatedAtAction("GetNewsCategoryById", id, newsCategoryResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<NewsCategoryResponse>>> GetAllNewsCategory()
		{
			var listNewsCategory = await _newsCategoryServices.GetAllNewsCategories();
			return Ok(listNewsCategory);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<NewsCategoryResponse>> GetNewsCategoryById(int id)
		{
			var newsCategory = await _newsCategoryServices.GetCategoryById(id);
			if (newsCategory == null) return NotFound();
			return Ok(newsCategory);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteNewsCategory(int id)
		{
			var result = await _newsCategoryServices.DeleteCategory(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<NewsCategoryResponse>> UpdateNewsCategory(NewsCategoryUpdateRequest newsCategoryUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var newsCategoryUpdate = await _newsCategoryServices.UpdateNewsCategories(newsCategoryUpdateRequest);
				return Ok(newsCategoryUpdate);
			}
			return BadRequest();
		}
	}
}
