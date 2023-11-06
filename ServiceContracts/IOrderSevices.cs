using Entities.Models;
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

		/// <summary>
		/// Get the total of order base on the From day and to day
		/// </summary>
		/// <param name="from">The from day</param>
		/// <param name="to">The to day</param>
		/// <param name="ticketId">The ticketId to search</param>
		/// <returns>A Total of mathcing order that has pruchase day inside the from and to day</returns>
		Task<double> GetTotalByDay(DateTime from, DateTime to, int ticketId);

		/// <summary>
		/// Get how many ticket has been bought
		/// </summary>
		/// <param name="from">The from day</param>
		/// <param name="to">The to day</param>
		/// <param name="ticketId">The ticketId to search(default = -1)</param>
		/// <returns>all of the total ticket has been bought</returns>
		Task<int> GetRevenue(DateTime from, DateTime to, int ticketId = -1);

		/// <summary>
		/// Get all the orderDetails between the given From and To date
		/// </summary>
		/// <param name="from">The from date</param>
		/// <param name="to">The to date</param>
		/// <returns>A list of all the orderDetails between the given From and To date</returns>
		Task<List<OrderDetailResponse>> GetOrderDetailsByDate(DateTime from, DateTime to);

		///<summary>
		/// Get list of order detail between the From and To day and have the same ticketId
		/// </summary>
		/// <param name="from">The from day</param>
		/// <param name="to">The to day</param>
		/// <param name="ticketId">The ticketId user want to search</param>
		/// <returns>A list of matching orderDetails object that between the given date</returns>
		Task<List<OrderDetailResponse>> GetOrderDetailByDate(DateTime from, DateTime to, int ticketId);
		Task<List<OrderResponse>> GetOrderByDate(DateTime from, DateTime to);
		Task<List<OrderResponse>> GetOrderByDate(DateTime from, DateTime to, int ticketId);
	}
}
