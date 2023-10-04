using Entities.Models;
using ServiceContracts.DTO.TicketDTO;

namespace ServiceContracts.DTO.OrderDTO
{
	public class OrderDetailResponse
	{
		public TicketResponse? Ticket { get; set; }

		public int Quantity { get; set; }

		public double TotalPrice { get; set; }
	}

	public static class OrderDetailExtension
	{
		public static OrderDetailResponse ToOrderDetailResopnse(this OrderDetail orderDetail)
		{
			return new OrderDetailResponse()
			{
				Ticket = orderDetail.Ticket?.ToTicketResponse(),
				Quantity = orderDetail.Quantity,
				TotalPrice = orderDetail.TotalPrice
			};
		}
	}
}
