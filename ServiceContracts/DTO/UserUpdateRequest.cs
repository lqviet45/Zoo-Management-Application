using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
	public class UserUpdateRequest
	{
		[Required(ErrorMessage = "UserId can not be blank!")]
		public long UserId { get; set; }

		[Required(ErrorMessage = "UserName Can not be blank!")]
		public string? UserName { get; set; }

		[Required]
		[StringLength(80)]
		public string? FullName { get; set; }

		[Required(ErrorMessage = "Email can not be blank!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Gender can not be blank!")]
		//[JsonConverter(typeof(StringEnumConverter))]
		public string? Gender { get; set; }

		[Required]
		public bool IsDelete { get; set; }

		[Required(ErrorMessage = "Phone number can not be blank!")]
		[Phone(ErrorMessage = "Invalid phone number!")]
		[StringLength(12)]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Date of birth can not be blank!")]
		public DateTime DateOfBirth { get; set; }

		public ExperienceAddRequest? ExperienceAddRequest { get; set; }

		public User MapToUser()
		{
			return new User
			{
				UserId = UserId,
				UserName = UserName,
				FullName = FullName,
				Email = Email,
				Gender = Gender,
				PhoneNumber = PhoneNumber,
				DateOfBirth = DateOfBirth,
				IsDelete = IsDelete,
			};
		}
	}
}
