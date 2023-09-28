using Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ServiceContracts.DTO.TicketDTO
{
	public class TicketUpdateRequest
	{
		[Required]
		public int TicketId { get; set; }

		[StringLength(80)]
		[Required(ErrorMessage = "Ticket Name can not be blank!")]
		public string? TicketName { get; set; }

		[Required(ErrorMessage = "Ticket price can not be blank!")]
		public double Price { get; set; }

		public Ticket MapToTicket()
		{
			return new Ticket()
			{
				TicketId = TicketId,
				TicketName = TicketName,
				Price = Price
			};
		}
	}
}
