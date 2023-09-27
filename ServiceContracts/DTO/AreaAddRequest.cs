using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Act as a DTO class for adding new Area
	/// </summary>
	public class AreaAddRequest
	{
		[Required(ErrorMessage = "AreaName can not be blank!")]
		public string AreaName { get; set; } = string.Empty;

		/// <summary>
		/// Converts the current object of AreaAddRequest into a new object of Area type
		/// </summary>
		/// <returns></returns>
		public Area MapToArea()
		{
			return new Area()
			{
				AreaName = AreaName
			};
		}
	}
}
