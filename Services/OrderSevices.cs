using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.OrderDTO;
using Services.Helper;

namespace Services
{
	public class OrderSevices : IOrderSevices
	{
		private readonly IOrderReponsitories _orderReponsitories;
		private readonly ITicketReponsitories _ticketReponsitories;

		public OrderSevices(IOrderReponsitories orderReponsitories, ITicketReponsitories ticketReponsitories)
		{
			_orderReponsitories = orderReponsitories;
			_ticketReponsitories = ticketReponsitories;
		}

		public async Task<OrderResponse> AddOrder(OrderAddRequest? orderAddRequest)
		{
			if (orderAddRequest is null)
			{
				throw new ArgumentNullException("The order adding is empty!");
			}

			ValidationHelper.ModelValidation(orderAddRequest);
			var order = orderAddRequest.MaptoOrder();

			await _orderReponsitories.Add(order);

			return order.ToOrderResopnse();
		}

		public async Task<List<OrderDetailResponse>> AddOrderDetail(List<OrderDetailAddRequest> orderDetailAddRequests)
		{
			ValidationHelper.ModelValidation(orderDetailAddRequests);

			var orderDetail = orderDetailAddRequests.Select(od => od.MapToOrderDetail()).ToList();
			orderDetail.ForEach(od =>
			{
				double total = 0;
				var ticket = _ticketReponsitories.GetTicketById(od.TicketId).Result;
				if (ticket is not null)
					total += od.Quantity * ticket.Price;

				od.TotalPrice = total;
			});
			await _orderReponsitories.AddOrderDetail(orderDetail);

			var orderDetailResponseList = orderDetail.Select(o => o.ToOrderDetailResopnse()).ToList();

			return orderDetailResponseList;
		}

		public async Task<OrderResponse?> GetOrderById(long orderId)
		{
			var orderResponse = await _orderReponsitories.GetOrderById(orderId);
			if (orderResponse is null) return null;

			return orderResponse.ToOrderResopnse();
		}

		public async Task UpdateOrderTotal(long orderId, double total)
		{
			var orderResponse = await _orderReponsitories.GetOrderById(orderId);
			if (orderResponse is null) return;
			await _orderReponsitories.UpdateOrderTotal(orderId, total);
		}

		public async Task<double> GetTotalByDay(DateTime from, DateTime to)
		{
			double total = await _orderReponsitories.GetTotalByDay(from, to);
			return total;
		}

		public async Task<List<OrderDetailResponse>> GetOrderDetailsByDate(DateTime from, DateTime to)
		{
			var listOrderDetail = await _orderReponsitories.GetOrderDeatilByDate(from, to);
			
			var listResponse = listOrderDetail.Select(od => od.ToOrderDetailResopnse()).ToList();

			return listResponse;
		}
	}
}
