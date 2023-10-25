using Entities.Models;

namespace ServiceContracts.DTO.OrderDTO
{
	public class OrderResponse
	{
		public long OrderId { get; set; }

		public DateTime PurchaseDate { get; set; }
		public virtual Custommer? Custommer { get; set; }

		public double TotalPrice { get; set; }

		public List<OrderDetailResponse> OrderDetailResponses { get; set; } = new List<OrderDetailResponse>();
	}

	public static class OrderExtension
	{
		public static OrderResponse ToOrderResopnse(this Order order)
		{
			return new OrderResponse()
			{
				OrderId = order.OrderId,
				PurchaseDate = order.PurchaseDate,
				Custommer = order.Custommer,
				TotalPrice = order.TotalPrice,
				OrderDetailResponses = order.OrderDetails.Select(od => od.ToOrderDetailResopnse()).ToList()
			};
		}
	}
}
