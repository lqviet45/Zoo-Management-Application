﻿using ServiceContracts.DTO.OrderDTO;

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
	}
}
