using Entities.Models;
using System.Linq.Expressions;

namespace RepositoryContracts
{
	/// <summary>
	/// Represents data access logic for managing News entity
	/// </summary>
	public interface INewsRepositories
	{
		/// <summary>
		/// Adds a News object to the data store
		/// </summary>
		/// <param name="news">The news to add</param>
		/// <returns>News obj after adding</returns>
		Task<News> Add(News news);

		/// <summary>
		/// Get all the news in the dataset
		/// </summary>
		/// <returns>A list of News obj</returns>
		Task<List<News>> GetAllNews();

		/// <summary>
		/// Get news by id in the dataset
		/// </summary>
		/// <param name="id">The id of the news</param>
		/// <returns>Matched news</returns>
		Task<News?> GetNewsById(int id);

		/// <summary>
		/// Get news by title in the dataset
		/// </summary>
		/// <param name="title">The title of the news</param>
		/// <returns>Matched title</returns>
		Task<News?> GetNewsByTitle(string title);

		/// <summary>
		/// Update an existed News
		/// </summary>
		/// <param name="news">The news to update</param>
		/// <returns>A news after Updated</returns>
		Task<News> UpdateNews(News news);

		/// <summary>
		/// Detele an existed news by id
		/// </summary>
		/// <param name="newsId">The id to delete</param>
		/// <returns>Returns true if delete is success,otherwise else False</returns>
		Task<bool> DeleteNews(int newsId);

		/// <summary>
		/// Returns all news obj base on the given expression
		/// </summary>
		/// <param name="predicate">Linq expression to check</param>
		/// <returns>All matching News with given condition</returns>
		Task<List<News>> GetFilteredNews(Expression<Func<News, bool>> predicate);

		/// <summary>
		/// Change the Active status of Deleted news
		/// </summary>
		/// <param name="newsId">The news id</param>
		/// <returns>Return true if successfully recovery the news, else return false</returns>
		Task<bool> RecoveryNews(int newsId);

		/// <summary>
		/// Get all the delete news
		/// </summary>
		/// <returns>Return list of deleted news</returns>
		Task<List<News>> GetAllDeleteNews();

		/// <summary>
		/// Returns all news obj base on the given expression
		/// </summary>
		/// <param name="predicate">Linq expression to check</param>
		/// <returns>All matching News with given condition</returns>
		Task<List<News>> GetCustomerSiteNews(Expression<Func<News, bool>> predicate);

		/// <summary>
		/// Get all the news in the dataset
		/// </summary>
		/// <returns>A list of News obj</returns>
		Task<List<News>> GetAllNewsStaffSite();
	}
}
