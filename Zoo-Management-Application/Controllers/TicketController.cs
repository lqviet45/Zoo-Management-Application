using Microsoft.AspNetCore.Authorization;
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

			return CreatedAtAction("GetTicketById", new {ticketId = ticketResponse.TicketId}, ticketResponse);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTicket()
		{
			var listTicket = await _ticketServices.GetAllTicket();

			return Ok(listTicket);
		}

		[HttpPut]
		public async Task<IActionResult> PutTicket(TicketUpdateRequest ticketUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var ticketUpdate = await _ticketServices.UpdateTicket(ticketUpdateRequest);

				return Ok(ticketUpdate);
			}

			return NotFound();
		}

		[HttpGet("{ticketId}")]
		public async Task<ActionResult<TicketResponse>> GetTicketById(int ticketId)
		{
			var ticket = await _ticketServices.GetTicketById(ticketId);
			if (ticket is null) return NotFound("The given ticket Id doesn't exist!");

			return Ok(ticket);
		}

		[HttpDelete("{ticketId}")]
		public async Task<IActionResult> DeleteTicket(int ticketId)
		{
			var ticket = await _ticketServices.GetTicketById(ticketId);
			if (ticket is null) return NotFound($"The ticket Id: {ticketId} doesn't exist!");

			var isDelete = await _ticketServices.DeleteTicket(ticketId);

			if (!isDelete) return BadRequest("Can not delete by some error!");

			return NoContent();
		}
	}
}
