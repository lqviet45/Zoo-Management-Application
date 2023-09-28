using ServiceContracts.DTO.OrderDTO;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

		public OrderAddRequest? OrderAddRequest { get; set; }
	}
}
