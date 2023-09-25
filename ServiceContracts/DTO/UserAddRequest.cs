using Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Act as a DTO class for adding new User
	/// </summary>
	public class UserAddRequest
	{
		[Required(ErrorMessage = "UserName Can not be blank!")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Email can not be blank!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string? Email { get; set;}

		[Required(ErrorMessage = "Full Name can not be blank!")]
		public string? FullName { get; set; }

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
		public ExperienceAddRequest? ExperienceAddRequest { get; set; }

		/// <summary>
		/// Convert UserAddRequest to User
		/// </summary>
		/// <returns>User object base on UserAddRequest</returns>
		public User MapToUser()
		{
			return new User()
			{
				UserName = UserName,
				Email = Email,
				PhoneNumber = PhoneNumber,
				FullName = FullName,
				Password = Password,
				DateOfBirth = DateOfBirth,
				RoleId = RoleId,
				Gender = Gender
			};
		}
	}
}
