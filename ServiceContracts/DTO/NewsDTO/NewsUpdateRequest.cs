using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.NewsDTO
{
	/// <summary>
	/// Represents the DTO class that contains the news details to update
	/// </summary>
	public class NewsUpdateRequest
	{
		[Required(ErrorMessage = "News ID can not be blank!")]
		public int NewsId { get; set; }

		[Required(ErrorMessage = "Title can not be blank!")]
		public string Title { get; set; } = string.Empty;

		public byte[]? Thumnail { get; set; }

		public byte[]? Image { get; set; }

		[Required(ErrorMessage = "Content can not be blank!")]
		public string? Content { get; set; }

		public string? Author { get; set; }	

		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessage = "Category ID can not be blank!")]
		public int CategoryId { get; set; }

		public virtual NewsCategories? NewsCategories { get; set; }

		/// <summary>
		/// Converts the current object of NewsAddRequest into a new object of News type
		/// </summary>
		/// <returns>Returns News object</returns>
		public News MapToNews()
		{
			return new News
			{
				NewsId = NewsId,
				Title = Title,
				Thumnail = Thumnail,
				Image = Image,
				Content = Content,
				Author = Author,
				ReleaseDate = ReleaseDate,
				CategoryId = CategoryId,
			};
		}
	}
}
