using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public string Content { get; set; } = string.Empty;

		[Required(ErrorMessage = "Author can not be blank!")]
		public string Author { get; set; } = string.Empty;

		[Required(ErrorMessage = "Category Id can not be blank!")]
		public int CategoryId { get; set; }
		public DateTime ReleaseDate { get; set; }
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
				ReleaseDate = ReleaseDate
			
			};
		}
		
	}
}
