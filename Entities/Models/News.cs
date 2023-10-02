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
		public byte[]? Thumnail { get; set; }
		public byte[]? Image { get; set; }
		public string? Content { get; set; }
		public string? Author { get; set; }
		public DateTime ReleaseDate { get; set; }

		[ForeignKey("NewsCategories")]
		[NotNull]
		public int? CategoryId { get; set; }

		public NewsCategories NewsCategories { get; set; } = null!;

	}
}
