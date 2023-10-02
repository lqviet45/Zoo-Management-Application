using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class NewsRepositories : INewsRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;
		

		// constructor
		public NewsRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		
		}

		public async Task<News> Add(News news)
		{
			if(news is null) throw new ArgumentNullException(nameof(news));

			_dbContext.News.Add(news);

			await _dbContext.SaveChangesAsync();

			return news;
		}

		public async Task<bool> DeleteNews(int newsId)
		{
			var deleteNews = await _dbContext.News.Where
							(n => n.NewsId == newsId).FirstAsync();

			if (deleteNews is null)
			{
				return false;
			}

			 _dbContext.News.Remove(deleteNews);
			int rowDeleted = await _dbContext.SaveChangesAsync();
			return rowDeleted > 0;
		}

		public async Task<List<News>> GetAllNews()
		{
			var listNews = await _dbContext.News.ToListAsync();

			return listNews;
		}

		public async Task<News?> GetNewsById(int id)
		{
			var news = await _dbContext.News.Where(n => n.NewsId == id).FirstOrDefaultAsync();

			return news;
		}

		public async Task<News?> GetNewsByTitle(string title)
		{
			var news = await _dbContext.News.Where(n => n.Title == title).FirstOrDefaultAsync();

		

			return news;
		}

		public async Task<News> UpdateNews(News news)
		{
			News? updateNews = await _dbContext.News.Where
						(n => n.NewsId == news.NewsId).FirstOrDefaultAsync();

			if (updateNews is null) return news;

			updateNews.Author = news.Author;
			updateNews.Title = news.Title;
			updateNews.Content = news.Content;
			updateNews.Thumnail = news.Thumnail;
			updateNews.CategoryId = news.CategoryId;
			updateNews.Image = news.Image;
			updateNews.ReleaseDate = news.ReleaseDate;

			int countUpdated = await _dbContext.SaveChangesAsync();

			return countUpdated > 0 ? updateNews : news;
		}
	}
}
