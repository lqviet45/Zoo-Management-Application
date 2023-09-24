using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
	public class UserUpdateRequest
	{
		[Required(ErrorMessage = "UserName Can not be blank!")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Email can not be blank!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Gender can not be blank!")]
		//[JsonConverter(typeof(StringEnumConverter))]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Phone number can not be blank!")]
		[Phone(ErrorMessage = "Invalid phone number!")]
		[StringLength(12)]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Password can not be blank!")]
		[MaxLength(20, ErrorMessage = "Password has max lenght is 20 character!")]
		public string? Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword can not be blank!")]
		[Compare("Password", ErrorMessage = "Password and confirm password is not match!")]
		public string? ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Date of birth can not be blank!")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Role can not be empty!")]
		public int RoleId { get; set; }
		public Experience? Experience { get; set; }
	}
}
