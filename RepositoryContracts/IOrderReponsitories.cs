using Entities.Models;

namespace RepositoryContracts
{
	public interface IOrderReponsitories
	{
		/// <summary>
		/// Adding a new order to database
		/// </summary>
		/// <param name="order">The order to add</param>
		/// <returns>A order object after added</returns>
		Task<Order> Add(Order order);

		/// <summary>
		/// Get order by Order id
		/// </summary>
		/// <param name="id">The order Id to get</param>
		/// <returns>A matching order or null</returns>
		Task<Order?> GetOrderById(long id);

		/// <summary>
		/// Adding list of orderDetail to database
		/// </summary>
		/// <param name="orderDetails">The list of orderDetail to add</param>
		/// <returns>A list of OrderDetail</returns>
		Task<List<OrderDetail>> AddOrderDetail(List<OrderDetail> orderDetails);

		/// <summary>
		/// Update Total price for order
		/// </summary>
		/// <param name="orderId">The orderId to update</param>
		/// <param name="total">The total to update</param>
		/// <returns></returns>
		Task UpdateOrderTotal(long orderId, double total);

		/// <summary>
		/// Get the total of order base on the From day and to day
		/// </summary>
		/// <param name="from">The from day</param>
		/// <param name="to">The to day</param>
		/// <returns>A Total of order that has pruchase day inside the from and to day</returns>
		Task<double> GetTotalByDay(DateTime from, DateTime to);
	}
}
