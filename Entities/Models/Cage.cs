using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Cage
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CageId { get; set; }

		[StringLength(20)]
		[NotNull]
		public string CageName { get; set; } = string.Empty;

		[NotNull]
		public bool IsDelete { get; set; }

		[ForeignKey("Area")]
		[NotNull]
		public int AreaId { get; set; }
		public Area Area { get; set; } = null!;
	}
}
