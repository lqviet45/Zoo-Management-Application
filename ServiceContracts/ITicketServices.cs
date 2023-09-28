

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
	}
}
