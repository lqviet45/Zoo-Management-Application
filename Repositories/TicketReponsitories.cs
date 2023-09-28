

using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryContracts
{
	public class TicketReponsitories : ITicketReponsitories
	{
		private readonly ApplicationDbContext _context;

		public TicketReponsitories(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Ticket> Add(Ticket ticket)
		{
			_context.Tickets.Add(ticket);
			await _context.SaveChangesAsync();
			return ticket;
		}

		public Task<bool> Delete(int ticketId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Ticket>> GetAllTicket()
		{
			var ticketList = await _context.Tickets.ToListAsync();

			return ticketList;
		}

		public Task<Ticket?> GetTicketById(int ticketId)
		{
			throw new NotImplementedException();
		}
	}
}
