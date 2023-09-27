

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Entities.Models;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Act as a DTO class for adding new Cage
	/// </summary>
	public class CageAddRequest
	{
		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Area Id can not be blank!")]
		public int AreaId { get; set; }

		/// <summary>
		/// Converts the current object of CageAddRequest into a new object of Cage type
		/// </summary>
		/// <returns></returns>
		public Cage MapToCage()
		{
			return new Cage()
			{
				CageName = CageName,
				AreaId = AreaId
			};
		}
	}
}
