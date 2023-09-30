﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
	public class AnimalFood
	{
		[ForeignKey("Animal")]
        public  long AnimalId { get; set; }
		[ForeignKey("Food")]
        public int FoodId { get; set; }

		public string? Note { get; set; }
		[NotNull]
		public DateTime FeedingTime { get; set; }

        public virtual Animal? Animal { get; set; }

        public virtual Food? Food { get; set; }
    }
}