using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.NewsDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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
		public async Task<ActionResult<NewsResponse>> PostNews([FromForm]NewsAddrequest newsAddRequest)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}
			var newsResponse = await _newsServices.AddNews(newsAddRequest);
			var id = new { id = newsResponse.NewsId };

			return CreatedAtAction("GetNewsById", id, newsResponse);
		}

		[HttpGet]
		public async Task<ActionResult<List<NewsResponse>>> GetAllNews()
		{
			var listNews = await _newsServices.GetAllNews();
			return Ok(listNews);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<NewsResponse>> GetNewsById(int id)
		{
			var news = await _newsServices.GetNewsById(id);
			if (news == null) return NotFound();
			return Ok(news);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteNews(int id)
		{
			var result = await _newsServices.DeleteNews(id);
			if (result == false) return NotFound();
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<NewsResponse>> UpdateNews(NewsUpdateRequest newsUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var newsUpdate = await _newsServices.UpdateNews(newsUpdateRequest);
				return Ok(newsUpdate);
			}
			return BadRequest();
		}

	}
}
