using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.OrderDTO;
using Services.Helper;

namespace Services
{
	public class OrderSevices : IOrderSevices
	{
		private readonly IOrderReponsitories _orderReponsitories;

		public OrderSevices(IOrderReponsitories orderReponsitories, ITicketReponsitories ticketReponsitories)
		{
			_orderReponsitories = orderReponsitories;
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
	}
}
