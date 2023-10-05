using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class AnimalUser
	{
		
		public long AnimalId { get; set; }

		public long UserId { get; set; }

		public virtual Animal? Animal { get; set; }

		public virtual User? User { get; set; }
	}
}
