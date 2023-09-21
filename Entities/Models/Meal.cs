using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Meal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long MealId { get; set; }

		[ForeignKey("Animal")]
		[NotNull]
		public long AnimalId { get; set; }

		[ForeignKey("FeedingFood")]
		[NotNull]
		public int FoodId { get; set; }
		public string? Note { get; set; }
		[NotNull]
		public DateTime FeedingTime { get; set; }

		public virtual Animal? Animal { get; set; }

		public virtual FeedingFood? FeedingFood { get; set; }
	}
}
