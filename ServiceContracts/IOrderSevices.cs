using ServiceContracts.DTO.OrderDTO;

namespace ServiceContracts
{
	public interface IOrderSevices
	{
		/// <summary>
		/// Add a new order
		/// </summary>
		/// <param name="orderAddRequest">The order add request</param>
		/// <returns>A order after adding as OrderResponse type</returns>
		Task<OrderResponse> AddOrder(OrderAddRequest? orderAddRequest);

		/// <summary>
		/// Get an order by Id
		/// </summary>
		/// <param name="orderId">The order Id to get</param>
		/// <returns>A matching Order as OrderResponse or null</returns>
		Task<OrderResponse?> GetOrderById(long orderId);

		/// <summary>
		/// Adding list of orderDetail to database
		/// </summary>
		/// <param name="orderDetailAddRequests">The list of orderDetail to add</param>
		/// <returns>A list of OrderResponse</returns>
		Task<List<OrderDetailResponse>> AddOrderDetail(List<OrderDetailAddRequest> orderDetailAddRequests);

		/// <summary>
		/// Update the order total
		/// </summary>
		/// <param name="orderId">The orderId to update</param>
		/// <param name="total">The total to update</param>
		/// <returns></returns>
		Task UpdateOrderTotal(long orderId, double total);

		/// <summary>
		/// Get the order that have purchase day between From date and to date
		/// </summary>
		/// <param name="from">The from date</param>
		/// <param name="to">The to date</param>
		/// <returns>A total of all the order that purchase day between the given From and To date</returns>
		Task<double> GetTotalByDay(DateTime from, DateTime to);
	}
}
