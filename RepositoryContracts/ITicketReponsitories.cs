using Entities.Models;

namespace RepositoryContracts
{
	public interface ITicketReponsitories
	{
		/// <summary>
		/// Adding a new ticket to database
		/// </summary>
		/// <param name="ticket">The ticket to add</param>
		/// <returns>A ticket after added</returns>
		Task<Ticket> Add(Ticket ticket);

		/// <summary>
		/// Get all the ticket in database
		/// </summary>
		/// <returns>A list of ticket</returns>
		Task<List<Ticket>> GetAllTicket();

		/// <summary>
		/// Get a ticket by id
		/// </summary>
		/// <param name="ticketId">The ticket id to get</param>
		/// <returns>A matching ticket or null</returns>
		Task<Ticket?> GetTicketById(int ticketId);

		/// <summary>
		/// Delete a ticket by id
		/// </summary>
		/// <param name="ticketId">The ticket Id to delete</param>
		/// <returns>True if delete success, else false</returns>
		Task<bool> Delete(int ticketId);

		/// <summary>
		/// Update an existed ticket in the database
		/// </summary>
		/// <param name="ticket">the ticket updated</param>
		/// <returns>A ticket after updated</returns>
		Task<Ticket> Update(Ticket ticket);
	}
}
