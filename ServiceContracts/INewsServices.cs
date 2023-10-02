using ServiceContracts.DTO.NewsDTO;

namespace ServiceContracts
{
	public interface INewsServices
	{
		/// <summary>
		/// Adding the new News into the News table
		/// </summary>
		/// <param name="newsAddRequest">The news to add</param>
		/// <returns>NewsResponse object base on the news adding</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		Task<NewsResponse> AddNews(NewsAddrequest? newsAddRequest);

		/// <summary>
		/// Get All the News in the News table
		/// </summary>
		/// <returns>A list of News object as NewsResponse</returns>
		Task<List<NewsResponse>> GetAllNews();

		/// <summary>
		/// Get a News by Id
		/// </summary>
		/// <param name="newsId">The Id of news to get</param>
		/// <returns>Matching news object as NewsResponse type</returns>
		Task<NewsResponse?> GetNewsById(int newsId);

		/// <summary>
		/// Updates the specified news details based on the given news ID
		/// </summary>
		/// <param name="newsUpdateRequest">News details to update</param>
		/// <returns>Returns the news response object updated</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		Task<NewsResponse> UpdateNews(NewsUpdateRequest? newsUpdateRequest);

		/// <summary>
		/// Delete exsiting news by news id
		/// </summary>
		/// <param name="newsId">The news id to delete</param>
		/// <returns>Returns true if delete success,otherwise return false</returns>
		Task<bool> DeleteNews(int newsId);
	}
}
