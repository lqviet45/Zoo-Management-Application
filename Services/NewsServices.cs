using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.NewsDTO;
using Services.Helper;

namespace Services
{
	public class NewsServices : INewsServices
	{
		// private fields
		private readonly INewsRepositories _newsRepositories;

		// constructor
		public NewsServices(INewsRepositories newsRepositories)
		{
			_newsRepositories = newsRepositories;
		}

		public async Task<NewsResponse> AddNews(NewsAddrequest? newsAddRequest)
		{
			ArgumentNullException.ThrowIfNull(newsAddRequest);

			var newsExist = await _newsRepositories.GetNewsByTitle(newsAddRequest.Title);
			if (newsExist is not null)
			{
				throw new ArgumentException("The news title is already exist!");
			}

			ValidationHelper.ModelValidation(newsAddRequest);

			News news = newsAddRequest.MapToNews();

			if (newsAddRequest.Image.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					newsAddRequest.Image.CopyTo(ms);
					var fileBytes = ms.ToArray();
					news.Image = fileBytes;
				}
			}

			if (newsAddRequest.Thumnail.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					newsAddRequest.Image.CopyTo(ms);
					var fileBytes = ms.ToArray();
					news.Thumnail = fileBytes;
				}
			}


			await _newsRepositories.Add(news);
			
			return news.ToNewsResponse();
		}

		public async Task<bool> DeleteNews(int newsId)
		{
			var existNews = await _newsRepositories.GetNewsById(newsId);
			
			if (existNews is null)
			{
				return false;
			}

			var isDeleted = await _newsRepositories.DeleteNews(newsId);

			return isDeleted;
		}

		public async Task<List<NewsResponse>> GetAllNews()
		{
			var listNews = await _newsRepositories.GetAllNews();

			var listNewsResponse = listNews.Select(news => news.ToNewsResponse()).ToList();

			return listNewsResponse;
		}

		public async Task<NewsResponse?> GetNewsById(int newsId)
		{
			var matchingNews = await _newsRepositories.GetNewsById(newsId);

			if(matchingNews is null || matchingNews.Image is null || matchingNews.Thumnail is null)
			{
				return null;
			}
			
			matchingNews.Image = GetImage(Convert.ToBase64String(matchingNews.Image));
			matchingNews.Thumnail = GetImage(Convert.ToBase64String(matchingNews.Thumnail));

			return matchingNews.ToNewsResponse();
		}



		public byte[]? GetImage(string sBase64String)
		{

			byte[]? bytes = null;
			if (!string.IsNullOrEmpty(sBase64String))
			{
				bytes = Convert.FromBase64String(sBase64String);
			}
			return bytes;
		}

		public async Task<NewsResponse> UpdateNews(NewsUpdateRequest? newsUpdateRequest)
		{
			if (newsUpdateRequest is null)
			{
				throw new ArgumentNullException(nameof(newsUpdateRequest));
			}

			ValidationHelper.ModelValidation(newsUpdateRequest);

			var updatedNews = await _newsRepositories.GetNewsById(newsUpdateRequest.NewsId);

			if (updatedNews is null)
			{
				throw new ArgumentException("The news is not exist!");
			}

			updatedNews.Title = newsUpdateRequest.Title;
			updatedNews.Content = newsUpdateRequest.Content;
			updatedNews.Image = newsUpdateRequest.Image;
			updatedNews.Thumnail = newsUpdateRequest.Thumnail;
			updatedNews.Author = newsUpdateRequest.Author;
			updatedNews.CategoryId = newsUpdateRequest.CategoryId;
			updatedNews.ReleaseDate = newsUpdateRequest.ReleaseDate;

			await _newsRepositories.UpdateNews(updatedNews);

			return updatedNews.ToNewsResponse();

		}
	}
}
