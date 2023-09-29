using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class OrderReponsitories : IOrderReponsitories
	{
		private readonly ApplicationDbContext _context;

		public OrderReponsitories(ApplicationDbContext applicationDbContext) 
		{
			_context = applicationDbContext;
		}

		public async Task<Order> Add(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<Order?> GetOrderById(long id)
		{
			var order = await _context.Orders
				.Include(o => o.OrderDetails)
				.ThenInclude(od => od.Select(od => od.Ticket))
				.FirstOrDefaultAsync(o => o.OrderId == id);

			return order;
		}
	}
}
