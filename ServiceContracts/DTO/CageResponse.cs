using Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Area service
	/// </summary>
	public class CageResponse
	{
		public string? CageId { get; set; }
		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;
		public int AreaId { get; set; }

		public virtual Area? Area { get; set; }
	}

	public static class CageExtension
	{
		/// <summary>
		/// A method to convert Cage to CageResponse
		/// </summary>
		/// <param name="cage">Cage to convert</param>
		/// <returns>CageResponse obj base on the cage</returns>
		public static CageResponse ToCageResponse(this Cage cage)
		{
			return new CageResponse()
			{
				CageId = cage.Area.AreaName + cage.CageId.ToString(),
				CageName = cage.CageName,
				AreaId = cage.AreaId,
				Area = cage.Area
			};
		}
	}
	

}
