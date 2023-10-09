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
			var listTicket = await _ticketReponsitories.GetAllTicket();
			orderDetails.ForEach(od =>
			{
				od.Ticket = listTicket.Find(t => t.TicketId == od.TicketId);
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

		public async Task<double> GetTotalByDay(DateTime from, DateTime to, int ticketId)
		{
			double total = await _context.OrderDetails
				.Include(od => od.Order)
				.Include(od => od.Ticket)
				.Where(o => o.Order != null && o.Order.PurchaseDate >= from && o.Order.PurchaseDate <= to && o.TicketId == ticketId)
				.SumAsync(o => o.TotalPrice);

			return total;
		}

		public async Task<List<OrderDetail>> GetOrderDeatilByDate(DateTime from, DateTime to)
		{
			List<OrderDetail> listOrderDetail = await _context.OrderDetails
				.Include(od => od.Order)
				.Include(od => od.Ticket)
				.Where(od => od.Order != null && od.Order.PurchaseDate >= from && od.Order.PurchaseDate <= to)
				.ToListAsync();

			return listOrderDetail;
		}

		public async Task<List<OrderDetail>> GetOrderDeatilByDate(DateTime from, DateTime to, int ticketId)
		{
			List<OrderDetail> listOrderDetail = await _context.OrderDetails
				.Include(od => od.Order)
				.Include(od => od.Ticket)
				.Where(od => od.Order != null && od.Order.PurchaseDate >= from && od.Order.PurchaseDate <= to && od.TicketId == ticketId)
				.ToListAsync();

			return listOrderDetail;
		}
	}
}
