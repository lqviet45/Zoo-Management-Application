using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.TransReportDTO
{
	public class DateSearch
	{
		[Required]
		public DateOnly From { get; set; }
		public DateOnly To { get; set; }
	}
}
