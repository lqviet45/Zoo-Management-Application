using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.TicketDTO;
using Services.Helper;

namespace Services
{
	public class TicketServices : ITicketServices
	{
		private readonly ITicketReponsitories _ticketReponsitories;

		public TicketServices(ITicketReponsitories ticketReponsitories)
		{
			_ticketReponsitories = ticketReponsitories;
		}

		public async Task<TicketResponse> AddTicket(TicketAddRequest? ticketAddRequest)
		{
			if (ticketAddRequest is null)
			{
				throw new ArgumentNullException("The ticket is null!");
			}

			ValidationHelper.ModelValidation(ticketAddRequest);

			var ticket = ticketAddRequest.MapToTicket();

			await _ticketReponsitories.Add(ticket);

			return ticket.ToTicketResponse();
		}

		public async Task<bool> DeleteTicket(int ticketId)
		{
			var existTicket = await _ticketReponsitories.GetTicketById(ticketId);
			if (existTicket is null)
			{
				throw new ArgumentNullException($"{ticketId} does not exist");
			}

			var isDelete = await _ticketReponsitories.Delete(existTicket.TicketId);

			return isDelete;
		}

		public async Task<List<TicketResponse>> GetAllTicket()
		{
			var listTicket = await _ticketReponsitories.GetAllTicket();

			return listTicket.Select(t => t.ToTicketResponse()).ToList();
		}

		public async Task<TicketResponse?> GetTicketById(int ticketId)
		{
			var existTicket = await _ticketReponsitories.GetTicketById(ticketId);

			if (existTicket is null) return null;

			return existTicket.ToTicketResponse();
		}

		public async Task<TicketResponse> UpdateTicket(TicketUpdateRequest? ticketUpdateRequest)
		{
			if (ticketUpdateRequest is null)
			{
				throw new ArgumentNullException("The ticket update is empty!");
			}

			ValidationHelper.ModelValidation(ticketUpdateRequest);

			var existTicket = await _ticketReponsitories.GetTicketById(ticketUpdateRequest.TicketId);

			if (existTicket is null)
			{
				throw new ArgumentException($"The Ticket Id: {ticketUpdateRequest.TicketId} doesn't exist!");
			}

			existTicket.TicketName = ticketUpdateRequest.TicketName;
			existTicket.Price = ticketUpdateRequest.Price;

			await _ticketReponsitories.Update(existTicket);

			return existTicket.ToTicketResponse();
		}
	}
}
