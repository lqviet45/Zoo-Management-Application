using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace ServiceContracts.DTO.NewsCategoriesDTO
{
	/// <summary>
	/// Act as a DTO class for adding new News Category
	/// </summary>
	public class NewsCategoryAddRequest
	{
		[Required(ErrorMessage = "Category Name can not be blank!")]
		public string CategoryName { get; set; } = string.Empty;

		/// <summary>
		/// Converts the current object of NewsCategoryAddRequest into a new object of NewsCategories type
		/// </summary>
		/// <returns></returns>
		public NewsCategories MapToNewsCategories()
		{
			return new NewsCategories()
			{
				CategoryName = CategoryName
			};
		}
	}
}
