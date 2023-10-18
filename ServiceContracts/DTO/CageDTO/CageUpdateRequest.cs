using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.CageDTO
{
	/// <summary>
	/// Represents the DTO class that contains the cage details to update
	/// </summary>
	public class CageUpdateRequest
	{
		[Required(ErrorMessage = "Cage ID can not be blank!")]
		public int CageId { get; set; }

		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Area ID can not be blank!")]
		public int AreaId { get; set; }

		public virtual Area? Area { get; set; }

		[Required]
		public bool IsDelete { get; set; }

		/// <summary>
		/// Converts the current object of CageAddRequest into a new object of Cage type
		/// </summary>
		/// <returns>Returns Cage object</returns>
		public Cage MapToCage()
		{
			return new Cage
			{
				CageId = CageId,
				CageName = CageName,
				AreaId = AreaId,
				IsDelete = IsDelete,
			};
		}
	}
}
