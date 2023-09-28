using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.OrderDTO
{
	public class OrderDetailAddRequest
	{
		[Required]
		public int TicketId { get; set; }

		[Required]
		public int OrderId { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
