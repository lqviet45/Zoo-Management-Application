using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.TicketDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class TicketController : ControllerBase
	{
		private readonly ITicketServices _ticketServices;

		public TicketController(ITicketServices ticketServices)
		{
			_ticketServices = ticketServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<TicketResponse>> PostTicket(TicketAddRequest ticketAddRequest)
		{
			var ticketResponse = await _ticketServices.AddTicket(ticketAddRequest);

			return CreatedAtAction("GetTicketById", new {ticketId = ticketResponse.TicketId}, ticketResponse);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllTicket()
		{
			var listTicket = await _ticketServices.GetAllTicket();

			return Ok(listTicket);
		}

		[HttpPut]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> PutTicket(TicketUpdateRequest ticketUpdateRequest)
		{
			if (ModelState.IsValid)
			{
				var ticketUpdate = await _ticketServices.UpdateTicket(ticketUpdateRequest);

				return Ok(ticketUpdate);
			}

			return NotFound();
		}

		[HttpGet("{TicketId}")]
		[AllowAnonymous]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Ticket>), Arguments = new object[] { "TicketId", typeof(int) })]
		public async Task<ActionResult<TicketResponse>> GetTicketById(int TicketId)
		{
			var ticket = await _ticketServices.GetTicketById(TicketId);
			if (ticket is null) return NotFound("The given ticket Id doesn't exist!");

			return Ok(ticket);
		}

		[HttpDelete("{TicketId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Ticket>), Arguments = new object[] { "TicketId", typeof(int) })]
		public async Task<IActionResult> DeleteTicket(int TicketId)
		{
			var ticket = await _ticketServices.GetTicketById(TicketId);
			if (ticket is null) return NotFound($"The ticket Id: {TicketId} doesn't exist!");

			var isDelete = await _ticketServices.DeleteTicket(TicketId);

			if (!isDelete) return BadRequest("Can not delete by some error!");

			return NoContent();
		}
	}
}
