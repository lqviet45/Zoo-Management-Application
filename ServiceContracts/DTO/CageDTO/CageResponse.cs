using Entities.Models;
using ServiceContracts.DTO.AreaDTO;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO.CageDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Cage service
	/// </summary>
	public class CageResponse
	{
		public int? CageId { get; set; }
		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;

		public AreaResponse? Area { get; set; }
	}

	public static class CageExtension
	{
		/// <summary>
		/// An extension method to convert an object of Cage class into CageResponse class
		/// </summary>
		/// <param name="cage">The Cage object to convert</param>
		/// /// <returns>Returns the converted CageResponse object</returns>
		public static CageResponse ToCageResponse(this Cage cage)
		{
			return new CageResponse()
			{
				CageId = cage.CageId,
				CageName = cage.CageName,
				Area = cage.Area.ToAreaResponse()
			};
		}
	}
	

}
