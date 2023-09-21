using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class Area
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AreaId { get; set; }

		[StringLength(20)]
		[NotNull]
		public string AreaName { get; set; } = string.Empty;
	}
}
