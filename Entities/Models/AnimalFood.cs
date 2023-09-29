
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class AnimalFood
	{
		
        public  long AnimalId { get; set; }
		
        public int FoodId { get; set; }

		public string? Note { get; set; }
		[NotNull]
		public DateTime FeedingTime { get; set; }

        public virtual Animal? Animal { get; set; }

        public virtual List<Food> Food { get; set; } = new List<Food>();
    }
}
