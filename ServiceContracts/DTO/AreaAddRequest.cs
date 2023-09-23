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
		/// Convert AreaAddRequest to Area
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
