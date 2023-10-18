

using ServiceContracts.DTO.TicketDTO;

namespace ServiceContracts
{
	public interface ITicketServices
	{
		/// <summary>
		/// Adding a new ticket
		/// </summary>
		/// <param name="ticketAddRequest">The ticket to add</param>
		/// <returns>A ticket after adding</returns>
		Task<TicketResponse> AddTicket(TicketAddRequest? ticketAddRequest);

		/// <summary>
		/// Get all ticket from database
		/// </summary>
		/// <returns>A list of ticket as ticketResponse type</returns>
		Task<List<TicketResponse>> GetAllTicket();

		/// <summary>
		/// Update an existing ticket
		/// </summary>
		/// <param name="ticketUpdateRequest">the ticket to update</param>
		/// <returns>A ticketResponse object after updated</returns>
		Task<TicketResponse> UpdateTicket(TicketUpdateRequest? ticketUpdateRequest);

		/// <summary>
		/// Get a ticket by Id
		/// </summary>
		/// <param name="ticketId">The ticket Id to get</param>
		/// <returns>A ticket or null</returns>
		Task<TicketResponse?> GetTicketById(int ticketId);

		/// <summary>
		/// Delete a ticket by id
		/// </summary>
		/// <param name="ticketId">The ticket Id to delete</param>
		/// <returns>True if delete success, else false</returns>
		Task<bool> DeleteTicket(int ticketId);
	}
}
