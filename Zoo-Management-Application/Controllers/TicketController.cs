using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.TicketDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketController : ControllerBase
	{
		private readonly ITicketServices _ticketServices;

		public TicketController(ITicketServices ticketServices)
		{
			_ticketServices = ticketServices;
		}

		[HttpPost]
		public async Task<ActionResult<TicketResponse>> PostTicket(TicketAddRequest ticketAddRequest)
		{
			var ticketResponse = await _ticketServices.AddTicket(ticketAddRequest);

			return Ok(ticketResponse);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTicket()
		{
			var listTicket = await _ticketServices.GetAllTicket();

			return Ok(listTicket);
		}
	}
}
