using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalCageDTO
{
	/// <summary>
	/// Act as a DTO class for adding new AnimalCage
	/// </summary>
	public class AnimalCageAddRequest
	{
		[Required(ErrorMessage = "CageId Can not be blank!")]
		public int CageId { get; set; }

		[Required(ErrorMessage = "AnimalId Can not be blank!")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "DateIn Can not be blank!")]
		public DateTime? DayIn { get; set; }

		[Required(ErrorMessage = "IsIn Can not be blank!")]
		public bool IsIn { get; set; }

		/// <summary>
		/// Converts the current object of AnimalCageAddRequest into a new object of AnimalCage type
		/// </summary>
		/// <returns></returns>
		public AnimalCage MapToAnimalCage()
		{
			return new AnimalCage()
			{
				AnimalId = this.AnimalId,
				CageId = this.CageId,
				DayIn = (DateTime)DayIn,
				IsIn = true
			};
		}
	}
}
