﻿using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class OrderReponsitories : IOrderReponsitories
	{
		private readonly ApplicationDbContext _context;
		private readonly ITicketReponsitories _ticketReponsitories;

		public OrderReponsitories(ApplicationDbContext applicationDbContext, ITicketReponsitories ticketReponsitories) 
		{
			_context = applicationDbContext;
			_ticketReponsitories = ticketReponsitories;
		}

		public async Task<Order> Add(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<List<OrderDetail>> AddOrderDetail(List<OrderDetail> orderDetails)
		{
			_context.OrderDetails.AddRange(orderDetails);
			
			await _context.SaveChangesAsync();

			orderDetails.ForEach(od =>
			{
				od.Ticket = _ticketReponsitories.GetTicketById(od.TicketId).Result;
			});

			return orderDetails;
		}

		public async Task<Order?> GetOrderById(long id)
		{
			var order = await _context.Orders
				.Include(o => o.Custommer)
				.Include(o => o.OrderDetails)
				.ThenInclude(od => od.Ticket)
				.FirstOrDefaultAsync(o => o.OrderId == id);

			return order;
		}
	}
}
