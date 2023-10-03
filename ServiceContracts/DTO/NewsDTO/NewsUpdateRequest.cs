using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[NotMapped]
		public IFormFile? ThumnailFile { get; set; }

		[Required(ErrorMessage = "Content can not be blank!")]
		public string? Content { get; set; }

		public string? Author { get; set; }	

		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessage = "Category ID can not be blank!")]
		public int CategoryId { get; set; }

		

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
				Content = Content,
				Author = Author,
				ReleaseDate = ReleaseDate,
				CategoryId = CategoryId
			};
		}
	}
}
