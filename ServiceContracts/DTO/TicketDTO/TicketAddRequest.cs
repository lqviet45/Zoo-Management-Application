using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.TicketDTO
{
	public class TicketAddRequest
	{
		[StringLength(80)]
		[Required(ErrorMessage = "Ticket name can not be blank!")]
		public string? TicketName { get; set; }

		[Required(ErrorMessage = "price can not be blank!")]
		public double Price { get; set; }

		[Required(ErrorMessage = "Release date can not be blank!")]
		public DateTime ReleaseDate { get; set; } = DateTime.Now;

		public Ticket MapToTicket()
		{
			return new Ticket()
			{
				TicketName = TicketName,
				Price = Price,
				ReleaseDate = ReleaseDate
			};
		}
	}
}
