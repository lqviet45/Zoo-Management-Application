using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.OrderDTO
{
	public class OrderDetailAddRequest
	{
		[Required]
		public int TicketId { get; set; }

		[Required]
		public long OrderId { get; set; }

		[Required]
		public int Quantity { get; set; }

		public OrderDetail MapToOrderDetail()
		{
			return new OrderDetail()
			{
				TicketId = TicketId,
				OrderID = OrderId,
				Quantity = Quantity
			};
		}
	}
}
