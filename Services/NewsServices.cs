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
		private readonly IFirebaseStorageService _firebaseStorageService;
		private readonly IUserRepositories _userRepositories;
		private readonly INewsCategoriesRepositories _newsCategoriesRepositories;
		private const string _folder = "news";

		// constructor
		public NewsServices(INewsRepositories newsRepositories, IFirebaseStorageService firebaseStorageService, IUserRepositories userRepositories, INewsCategoriesRepositories newsCategoriesRepositories)
		{
			_newsRepositories = newsRepositories;
			_firebaseStorageService = firebaseStorageService;
			_userRepositories = userRepositories;
			_newsCategoriesRepositories = newsCategoriesRepositories;
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

			if(newsAddRequest.ImageFile != null && newsAddRequest.Title != null)
			{
				var imageUri = await _firebaseStorageService.UploadFile(newsAddRequest.Title, newsAddRequest.ImageFile, _folder);
				news.Image = imageUri;
			}

			if(newsAddRequest.ThumnailFile != null && newsAddRequest.Title != null)
			{
				var imageUri = await _firebaseStorageService.UploadFile(newsAddRequest.Title, newsAddRequest.ThumnailFile, _folder);
				news.Thumnail = imageUri;
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

			if(existNews.IsActive == false)
			{
				return false;
			}

			var isActive = await _newsRepositories.DeleteNews(newsId);

			return isActive;
		}

		public async Task<List<NewsResponse>> GetAllNews()
		{
			var listNews = await _newsRepositories.GetAllNews();

			listNews.OrderByDescending(n => n.ReleaseDate);

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

			news.OrderBy(n => n.Priority);

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

		public async Task<List<NewsResponse>> GetTop3News()
		{
			var list= await _newsRepositories.GetAllNews();
			var listNews = list.OrderByDescending(n => n.ReleaseDate)
				.Take(3);

			return listNews.Select(n => n.ToNewsResponse()).ToList();
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
			updatedNews.ReleaseDate = newsUpdateRequest.ReleaseDate;

			var existCategory = await _newsCategoriesRepositories.GetCategoryById(newsUpdateRequest.CategoryId);

			updatedNews.CategoryId = newsUpdateRequest.CategoryId;

			if (newsUpdateRequest.ImageFile != null && newsUpdateRequest.Title != null)
			{
				var imageUri = await _firebaseStorageService.UploadFile(newsUpdateRequest.Title, newsUpdateRequest.ImageFile, _folder);
				updatedNews.Image = imageUri;
			}

			if (newsUpdateRequest.ThumnailFile != null && newsUpdateRequest.Title != null)
			{
				var imageUri = await _firebaseStorageService.UploadFile(newsUpdateRequest.Title, newsUpdateRequest.ThumnailFile, _folder);
				updatedNews.Thumnail = imageUri;
			}

			await _newsRepositories.UpdateNews(updatedNews);

			return updatedNews.ToNewsResponse();

		}

		public async Task<bool> RecoveryNews(int newsId)
		{
			var recoveryNews = await _newsRepositories.GetNewsById(newsId);

			if (recoveryNews is null)
			{
				return false;
			}

			if (recoveryNews.IsActive == true)
			{
				return false;
			}

			var isActive = await _newsRepositories.RecoveryNews(newsId);

			return isActive;
		}

		public async Task<List<NewsResponse>> GetAllDeletedNews()
		{
			var listNews = await _newsRepositories.GetAllDeleteNews();

			var listNewsResponse = listNews.Select(news => news.ToNewsResponse()).ToList();

			return listNewsResponse;
		}

		public async Task<List<NewsResponse>> Get3ReletiveNews(int CategoryId)
		{
			var list = await _newsRepositories.GetAllNews();

			var listNews = list.Where(n => n.CategoryId == CategoryId);

			listNews = list.OrderBy(n => n.CategoryId)
							.ThenByDescending(n => n.ReleaseDate)
							.Take(3);

			return listNews.Select(n => n.ToNewsResponse()).ToList();
		}
	}
}
