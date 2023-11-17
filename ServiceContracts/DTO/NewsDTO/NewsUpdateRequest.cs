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
		[Required(ErrorMessage = "Author can not be blank!")]
		public string? Author { get; set; }
		[Required(ErrorMessage = "Release Date can not be blank!")]
		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessage = "Category ID can not be blank!")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Priority can not be blank!")]
		[Range(1, 5, ErrorMessage = "Priority must be between {1} and {2}")]
		public int Priority { get; set; }

		[Required(ErrorMessage = "Active Status can not be blank!")]
		public bool IsActive { get; set; }


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
				CategoryId = CategoryId,
				Priority = Priority,
				IsActive = IsActive
			};
		}
	}
}
