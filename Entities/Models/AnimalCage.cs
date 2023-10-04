using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class AnimalCage
	{
		[Key]
		public long AnimalId { get; set; }

		[Key]
		public int CageId { get; set; }
		[Column(TypeName = "Date")]
		public DateTime DayIn { get; set; }
		public virtual Animal? Animal { get; set; }
		public virtual Cage? Cage { get; set; }
	}
}
