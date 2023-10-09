using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalCageDTO
{
	/// <summary>
	/// Act as a DTO class for updating AnimalCage
	/// </summary>
	public class AnimalCageUpdateRequest
	{
		[Required(ErrorMessage = "CageId Can not be blank!")]
		public int CageId { get; set; }

		[Required(ErrorMessage = "AnimalId Can not be blank!")]
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "DateIn Can not be blank!")]
		[DataType(DataType.Date)]
		public DateTime DayIn { get; set; }

		[Required(ErrorMessage = "IsIn Can not be blank!")]
		public bool IsIn { get; set; }

		/// <summary>
		/// Converts the current object of AnimalCageUpdateRequest into a new object of AnimalCage type
		/// </summary>
		/// <returns></returns>
		public AnimalCage MapToAnimalCage()
		{
			return new AnimalCage()
			{
				AnimalId = this.AnimalId,
				CageId = this.CageId,
				DayIn = DayIn,
				IsIn = true
			};
		}
	}
}
