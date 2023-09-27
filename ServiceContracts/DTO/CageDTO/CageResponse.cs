using Entities.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ServiceContracts.DTO.AreaDTO;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO.CageDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Cage service
	/// </summary>
	public class CageResponse
	{
		public string? CageId { get; set; }
		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;
		public int AreaId { get; set; }

		public AreaResponse? Area { get; set; }
	}

	public static class CageExtension
	{
		/// <summary>
		/// An extension method to convert an object of Cage class into CageResponse class
		/// </summary>
		/// <param name="person">The Cage object to convert</param>
		/// /// <returns>Returns the converted CageResponse object</returns>
		public static CageResponse ToCageResponse(this Cage cage)
		{
			return new CageResponse()
			{
				CageId = cage.Area.AreaName + cage.CageId.ToString(),
				CageName = cage.CageName,
				AreaId = cage.AreaId,
				Area = cage.Area.ToAreaResponse()
			};
		}
	}
	

}
