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

		public async Task<List<TicketResponse>> GetAllTicket()
		{
			var listTicket = await _ticketReponsitories.GetAllTicket();

			return listTicket.Select(t => t.ToTicketResponse()).ToList();
		}
	}
}
