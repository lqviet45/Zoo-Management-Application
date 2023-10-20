using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Entities.Models
{
	public class Food
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		
		public int FoodId { get; set; }

		[NotNull]
		[StringLength(100)]
		public string? FoodName { get; set; }
		

		
	}
}
