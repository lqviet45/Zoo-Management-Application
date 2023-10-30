
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Entities.Models;

namespace ServiceContracts.DTO.TicketDTO
{
	public class TicketResponse
	{
		public int TicketId { get; set; }

		public string? TicketName { get; set; }

		public double Price { get; set; }

		public DateTime ReleaseDate { get; set; }

		public string? Image { get; set; }
	}

	public static class TicketExtension
	{
		/// <summary>
		/// Convert a ticket to ticketResponse
		/// </summary>
		/// <param name="ticket">The ticket to convert</param>
		/// <returns>A ticketResopnse base on the ticket</returns>
		public static TicketResponse ToTicketResponse(this Ticket ticket)
		{
			return new TicketResponse()
			{
				TicketId = ticket.TicketId,
				TicketName = ticket.TicketName,
				Price = ticket.Price,
				ReleaseDate = ticket.ReleaseDate,
				Image = ticket.Image
			};
		}
	}
}
