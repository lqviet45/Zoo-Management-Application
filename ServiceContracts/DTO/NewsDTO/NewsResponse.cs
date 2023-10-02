using Entities.Models;
using ServiceContracts.DTO.NewsCategoriesDTO;
using ServiceContracts.DTO.NewsDTO;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO.NewsDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of News service
	/// </summary>
	public class NewsResponse
	{
		public int NewsId { get; set; }
		[Required(ErrorMessage = "Title can not be blank!")]
		public string? Title { get; set; } = string.Empty;
		public string? Thumnail { get; set; }
		public string? Image { get; set; }
		[Required(ErrorMessage = "Content can not be blank!")]
		public string? Content { get; set; } = string.Empty;
		[Required(ErrorMessage = "Author can not be blank!")]
		public string? Author { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public int? CategoryId { get; set; }

		public NewsCategoryResponse? NewsCategories { get; set; }

	}


	public static class NewsExtension
	{
		/// <summary>
		/// An extension method to convert an object of News class into NewsResponse class
		/// </summary>
		/// <param name="news">The News object to convert</param>
		/// /// <returns>Returns the converted NewsResponse object</returns>
		public static NewsResponse ToNewsResponse(this News news)
		{
			return new NewsResponse()
			{
				NewsId = news.NewsId,
				Title = news.Title,
				Thumnail = news.Thumnail,
				Image = news.Image,
				Content = news.Content,
				Author = news.Author,
				ReleaseDate = news.ReleaseDate,
				CategoryId = news.CategoryId,
				NewsCategories = news.NewsCategories.ToNewsCategoryResponse()
			};
		}

	}

}
