using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.NewsCategoriesDTO
{
	/// <summary>
	/// Represents the DTO class that contains the NewsCategory details to update
	/// </summary>
	public class NewsCategoryUpdateRequest
	{
		[Required(ErrorMessage = "Category Id can not be blank!")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Category Name can not be blank!")]
		public string CategoryName { get; set; } = string.Empty;

		/// <summary>
		/// Converts the current object of NewsCategoryUpdateRequest into a new object of NewsCategories type
		/// </summary>
		/// <returns>Returns Area object</returns>
		public NewsCategories MapToNewsCategories()
		{
			return new NewsCategories()
			{
				CategoryId = CategoryId,
				CategoryName = CategoryName
			};
		}
	}
}
