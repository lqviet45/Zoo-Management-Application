using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.NewsCategoriesDTO
{
	public class NewsCategoryResponse
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "CategoryName can not be blank!")]
		public string? CategoryName { get; set; }
	}

	public static class NewsCategoryExtension
	{
		/// <summary>
		/// An extension method to convert an object of Categories class into CategoryResponse class
		/// </summary>
		/// <param name="newsCategories">The NewsCategories object to convert</param>
		/// /// <returns>Returns the converted CategoryResponse object</returns>
		public static NewsCategoryResponse ToNewsCategoryResponse(this NewsCategories newsCategories)
		{
			return new NewsCategoryResponse()
			{
				CategoryId = newsCategories.CategoryId,
				CategoryName = newsCategories.CategoryName
			};
		}
	}
}
