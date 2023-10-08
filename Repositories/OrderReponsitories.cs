using Entities.AppDbContext;
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

		public async Task UpdateOrderTotal(long orderId, double total)
		{
			var order = await GetOrderById(orderId);
			if (order is null) {
				return;
			}
			order.TotalPrice = total;
			await _context.SaveChangesAsync();
		}

		public async Task<double> GetTotalByDay(DateTime from, DateTime to)
		{
			double total = await _context.Orders
				.Where(o => o.PurchaseDate >=  from && o.PurchaseDate <= to)
				.SumAsync(o => o.TotalPrice);

			return total;
		}

		public async Task<List<OrderDetail>> GetOrderDeatilByDate(DateTime from, DateTime to)
		{
			List<OrderDetail> listOrderDetail = await _context.OrderDetails
				.Include(od => od.Order)
				.Where(od => od.Order != null && od.Order.PurchaseDate >= from && od.Order.PurchaseDate <= to)
				.ToListAsync();

			return listOrderDetail;
		}
	}
}
