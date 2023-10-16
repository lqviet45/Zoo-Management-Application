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
		private readonly IFileServices _fileServices;
		private readonly IUserRepositories _userRepositories;

		// constructor
		public NewsServices(INewsRepositories newsRepositories, IFileServices fileServices, IUserRepositories userRepositories)
		{
			_newsRepositories = newsRepositories;
			_fileServices = fileServices;
			_userRepositories = userRepositories;
		}

		public async Task<NewsResponse> AddNews(NewsAddrequest? newsAddRequest)
		{
			ArgumentNullException.ThrowIfNull(newsAddRequest);

			var newsExist = await _newsRepositories.GetNewsByTitle(newsAddRequest.Title);
			if (newsExist is not null)
			{
				throw new ArgumentException("The news title is already exist!");
			}

			var userExist = await _userRepositories.GetUserById(newsAddRequest.UserId);
			if (userExist is null)
			{
				throw new ArgumentException("The user is not exist!");	
			}

			ValidationHelper.ModelValidation(newsAddRequest);

			News news = newsAddRequest.MapToNews();

			if(newsAddRequest.ImageFile != null)
			{
				var fileResult = _fileServices.SaveImage(newsAddRequest.ImageFile);
				if(fileResult.Item1 == 1)
				{
					news.Image = fileResult.Item2; // getting name of image
				}
			}

			if(newsAddRequest.ThumnailFile != null)
			{
				var fileResult = _fileServices.SaveImage(newsAddRequest.ThumnailFile);
				if(fileResult.Item1 == 1)
				{
					news.Thumnail = fileResult.Item2; // getting name of thumbnail
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

			if (existNews is not null)
			{
				if (!string.IsNullOrEmpty(existNews.Image))
				{
					_fileServices.DeleteImage(existNews.Image);
				}

				if (!string.IsNullOrEmpty(existNews.Thumnail))
				{
					_fileServices.DeleteImage(existNews.Thumnail);
				}
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

		public async Task<List<NewsResponse>> GetFiteredNews(string searchBy, string? searchString)
		{
			if(string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<News> news = searchBy switch
			{
				nameof(NewsResponse.Title) =>
				await _newsRepositories.GetFilteredNews(temp =>
					temp.Title.Contains(searchString)),

				nameof(NewsResponse.Author) =>
				await _newsRepositories.GetFilteredNews(temp =>
					temp.Author.Contains(searchString)),

				nameof(NewsResponse.Content) =>
				await _newsRepositories.GetFilteredNews(temp =>
					temp.Content.Contains(searchString)),

				_ => await _newsRepositories.GetAllNews()
			};

			return news.Select(news => news.ToNewsResponse()).ToList();

		}

		public async Task<NewsResponse?> GetNewsById(int newsId)
		{
			var matchingNews = await _newsRepositories.GetNewsById(newsId);

			if(matchingNews is null)
			{
				return null;
			}
			
			return matchingNews.ToNewsResponse();
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
			updatedNews.Author = newsUpdateRequest.Author;
			updatedNews.CategoryId = newsUpdateRequest.CategoryId;
			updatedNews.ReleaseDate = newsUpdateRequest.ReleaseDate;
			updatedNews.UserId = newsUpdateRequest.UserId;

			if (newsUpdateRequest.ImageFile != null)
			{
				var fileResult = _fileServices.SaveImage(newsUpdateRequest.ImageFile);

				if (!string.IsNullOrEmpty(updatedNews.Image))
				{
					_fileServices.DeleteImage(updatedNews.Image); // delete old image
				}

				if (fileResult.Item1 == 1)
				{
					updatedNews.Image = fileResult.Item2; // getting name of image
				}

			}

			if (newsUpdateRequest.ThumnailFile != null)
			{
				var fileResult = _fileServices.SaveImage(newsUpdateRequest.ThumnailFile);

				if (!string.IsNullOrEmpty(updatedNews.Thumnail))
				{
					_fileServices.DeleteImage(updatedNews.Thumnail); // delete old image
				}

				if (fileResult.Item1 == 1)
				{
					updatedNews.Thumnail = fileResult.Item2; // getting name of thumbnail
				}
			}

			await _newsRepositories.UpdateNews(updatedNews);

			return updatedNews.ToNewsResponse();

		}
	}
}
