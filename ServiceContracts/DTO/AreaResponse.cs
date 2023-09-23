using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Area service
	/// </summary>
	public class AreaResponse
	{
		public int AreaId { get; set; }

		[Required(ErrorMessage = "AreaName can not be blank!")]
		public string? AreaName { get; set; }
	}

	public static class AreaExtension
	{
		/// <summary>
		/// A method to Convert Area To AreaResponse
		/// </summary>
		/// <param name="area">Area to convert</param>
		/// <returns>AreaResponse obj base on the area</returns>
		public static AreaResponse ToAreaResponse(this Area area)
		{
			return new AreaResponse()
			{
				AreaId = area.AreaId,
				AreaName = area.AreaName
			};
		}
	}

}

