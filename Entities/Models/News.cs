
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class News
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NewsId { get; set; }
		public string? Title { get; set; }
		public string? Thumnail { get; set; }
		public string? Image { get; set; }
		public string? Content { get; set; }
		public string? Author { get; set; }
		public DateTime ReleaseDate { get; set; }

		[ForeignKey("NewsCategories")]
		[NotNull]
		public int? CategoryId { get; set; }

		public NewsCategories NewsCategories { get; set; } = null!;

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[NotMapped]
		public IFormFile? ThumnailFile { get; set; } 

	}
}
