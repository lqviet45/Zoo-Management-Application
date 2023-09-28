

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

		public async Task<bool> Delete(int ticketId)
		{
			var ticketExist = await GetTicketById(ticketId);
			if (ticketExist is null) return false;

			_context.Tickets.Remove(ticketExist);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<List<Ticket>> GetAllTicket()
		{
			var ticketList = await _context.Tickets.ToListAsync();

			return ticketList;
		}

		public async Task<Ticket?> GetTicketById(int ticketId)
		{
			var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);

			return ticket;
		}

		public async Task<Ticket> Update(Ticket ticket)
		{
			var ticketExist = await GetTicketById(ticket.TicketId);
			if (ticketExist is null) return ticket;

			ticketExist.TicketName = ticket.TicketName;
			ticketExist.Price = ticket.Price;

			await _context.SaveChangesAsync();

			return ticketExist;
		}
	}
}
