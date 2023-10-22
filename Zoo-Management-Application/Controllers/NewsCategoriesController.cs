using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO. NewsCategoriesDTO;
using Zoo.Management.Application.Filters.ActionFilters;

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
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[Authorize(Roles = "OfficeStaff")]
		public async Task<ActionResult<NewsCategoryResponse>> PostNewsCategory(NewsCategoryAddRequest newsCategoryAddRequest)
		{
			var newsCategoryResponse = await _newsCategoryServices.AddNewsCategories(newsCategoryAddRequest);
			var CategoryId = new { CategoryId = newsCategoryResponse.CategoryId };

			return CreatedAtAction("GetNewsCategoryById", CategoryId, newsCategoryResponse);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<List<NewsCategoryResponse>>> GetAllNewsCategory()
		{
			var listNewsCategory = await _newsCategoryServices.GetAllNewsCategories();
			return Ok(listNewsCategory);
		}

		[HttpGet("{CategoryId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<NewsCategories>), Arguments = new object[] { "CategoryId", typeof(int) })]
		[AllowAnonymous]
		public async Task<ActionResult<NewsCategoryResponse>> GetNewsCategoryById(int CategoryId)
		{
			var newsCategory = await _newsCategoryServices.GetCategoryById(CategoryId);
			if (newsCategory == null) return NotFound();
			return Ok(newsCategory);
		}

		[HttpDelete("{CategoryId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<NewsCategories>), Arguments = new object[] { "CategoryId", typeof(int) })]
		[Authorize(Roles = "OfficeStaff")]
		public async Task<ActionResult<bool>> DeleteNewsCategory(int CategoryId)
		{
			var result = await _newsCategoryServices.DeleteCategory(CategoryId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[Authorize(Roles = "OfficeStaff")]
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
