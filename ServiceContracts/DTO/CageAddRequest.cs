

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Entities.Models;

namespace ServiceContracts.DTO
{
	public class CageAddRequest
	{
		[Required(ErrorMessage = "Cage Name can not be blank!")]
		public string CageName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Area Id can not be blank!")]
		public int AreaId { get; set; }

		public virtual Area Area { get; set; } = null!;

		/// <summary>
		/// Convert CageAddRequest to Cage
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
