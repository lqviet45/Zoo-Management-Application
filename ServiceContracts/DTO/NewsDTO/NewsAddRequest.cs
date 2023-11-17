using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts.DTO.NewsDTO
{
	/// <summary>
	/// Act as a DTO class for adding new News
	/// </summary>
	public class NewsAddrequest
	{
		[Required(ErrorMessage = "Title can not be blank!")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Content can not be blank!")]
		public string? Content { get; set; } = string.Empty;

		[Required(ErrorMessage = "Author can not be blank!")]
		public string? Author { get; set; } = string.Empty;

		[Required(ErrorMessage = "Category Id can not be blank!")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Release Date can not be blank!")]
		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessage = "User Id can not be blank!")]
		public long UserId { get; set; }

		[Required(ErrorMessage ="Priority can not be blank!")]
		[Range(1, 5, ErrorMessage ="Priority must be between {1} and {2}")]
		public int Priority { get; set; }

		[Required(ErrorMessage ="Active Status can not be blank!")]
		public bool IsActive { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[NotMapped]
		public IFormFile? ThumnailFile { get; set; }


		/// <summary>
		/// Converts the current object of NewsAddRequest into a new object of News type
		/// </summary>
		/// <returns></returns>
		public News MapToNews()
		{
			return new News()
			{
				Title = Title,
				Content = Content,
				Author = Author,
				CategoryId = CategoryId,
				ReleaseDate = ReleaseDate,
				UserId = UserId,
				Priority = Priority,
				IsActive = IsActive,
			};
		}
		
	}
}
