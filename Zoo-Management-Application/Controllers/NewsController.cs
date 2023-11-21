using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.NewsDTO;
using ServiceContracts.DTO.WrapperDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="OfficeStaff")]
	public class NewsController : ControllerBase
	{
		// private field
		private readonly INewsServices _newsServices;

		// constructor
		public NewsController(INewsServices newsServices)
		{
			_newsServices = newsServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<NewsResponse>> PostNews([FromForm]NewsAddrequest newsAddRequest)
		{
			var newsResponse = await _newsServices.AddNews(newsAddRequest);
			
			var NewsId = new { NewsId = newsResponse.NewsId };

			return CreatedAtAction("GetNewsById", NewsId, newsResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<NewsResponse>>> GetAllNews(int? pageNumber, string searchBy = "Title", string? searchString = null)
		{
			var listNews = await _newsServices.GetFiteredNews(searchBy, searchString);
			int pageSize = 6;
			var pagingList = PaginatedList<NewsResponse>.CreateAsync(listNews.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("customer-news")]
		[AllowAnonymous]
		public async Task<ActionResult<List<NewsResponse>>> GetAllNewsCustomer(int? pageNumber, string searchBy = "Title", string? searchString = null)
		{
			var listNews = await _newsServices.GetCustomerSiteNews(searchBy, searchString);
			int pageSize = 6;
			var pagingList = PaginatedList<NewsResponse>.CreateAsync(listNews.AsQueryable().AsNoTracking(), pageNumber ?? 1, pageSize);
			var response = new { pagingList, pagingList.TotalPages };
			return Ok(response);
		}

		[HttpGet("{NewsId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<News>), Arguments = new object[] { "NewsId", typeof(int) })]
		public async Task<ActionResult<NewsResponse>> GetNewsById(int NewsId)
		{
			var news = await _newsServices.GetNewsById(NewsId);
			if (news == null) return NotFound();
			return Ok(news);
		}

		[HttpDelete("{NewsId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<News>), Arguments = new object[] { "NewsId", typeof(int) })]
		public async Task<ActionResult<bool>> DeleteNews(int NewsId)
		{
			var result = await _newsServices.DeleteNews(NewsId);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<NewsResponse>> UpdateNews([FromForm]NewsUpdateRequest newsUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var newsUpdate = await _newsServices.UpdateNews(newsUpdateRequest);
				return Ok(newsUpdate);
			}
			return BadRequest();
		}

		[HttpGet("get-top-news")]
		[AllowAnonymous]
		public async Task<ActionResult<List<NewsResponse>>> GetTopNews()
		{
			var listNews = await _newsServices.GetTop3News();
			
			return Ok(listNews);
		}

		[HttpGet("get-relative-news")]
		[AllowAnonymous]
		public async Task<ActionResult<List<NewsResponse>>> GetRelativeNews(int NewsId)
		{
			var listNews = await _newsServices.Get3ReletiveNews(NewsId);

			return Ok(listNews);
		}

		[HttpGet("get-unactive-news")]
		public async Task<ActionResult<List<NewsResponse>>> GetUnActiveNews()
		{
			var listNews = await _newsServices.GetAllDeletedNews();

			return Ok(listNews);
		}

		[HttpPut("active-news")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<News>), Arguments = new object[] { "NewsId", typeof(int) })]
		public async Task<ActionResult<NewsResponse>> ActiveNews(int NewsId)
		{
			var result = await _newsServices.RecoveryNews(NewsId);

			if (result == false) return NotFound();

			return Ok(result);
		}

	}
}
