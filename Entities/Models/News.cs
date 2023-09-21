using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public DateOnly ReleaseDate { get; set; }



	}
}
