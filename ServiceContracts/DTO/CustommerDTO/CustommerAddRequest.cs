using Entities.Models;
using ServiceContracts.DTO.OrderDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.CustommerDTO
{
	public class CustommerAddRequest
	{
		[Required(ErrorMessage = "Custommer Name can not be blank!")]
		[StringLength(80)]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Email can not be blank!")]
		[StringLength(80)]
		[EmailAddress(ErrorMessage = "Please input a valid email address!")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Phone number can not be blank!")]
		[StringLength(15)]
		[Phone(ErrorMessage = "Plese input a valid phone number!")]
		public string? PhoneNumber { get; set; }

		public List<int> TicketIdList { get; set; } = new List<int>();

		public Custommer MapToCustommer()
		{
			return new Custommer()
			{
				Name = Name,
				Email = Email,
				PhoneNumber = PhoneNumber
			};
		}
	}
}
