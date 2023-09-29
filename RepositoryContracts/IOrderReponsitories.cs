﻿using Entities.Models;

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
	}
}
