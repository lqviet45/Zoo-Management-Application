
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
		[NotNull]
		[StringLength(100)]
		public string? Title { get; set; }
		[NotNull]
		[StringLength(100)]
		public string? Thumnail { get; set; }
		[NotNull]
		[StringLength(100)]
		public string? Image { get; set; }
		[NotNull]
		[MaxLength]
		public string? Content { get; set; }
		[NotNull]
		[StringLength(50)]
		public string? Author { get; set; }
		[Column(TypeName = "Date")]
		public DateTime ReleaseDate { get; set; }

		[ForeignKey("NewsCategories")]
		[NotNull]
		public int CategoryId { get; set; }

		public NewsCategories NewsCategories { get; set; } = null!;

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[NotMapped]
		public IFormFile? ThumnailFile { get; set; } 

	}
}
