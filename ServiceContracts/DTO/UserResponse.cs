using Entities.Models;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of User service
	/// </summary>
	public class UserResponse
	{
		public long UserId { get; set; }

		[Required(ErrorMessage = "UserName Can not be blank!")]
		public string? UserName { get; set; }
		[Required(ErrorMessage = "Email can not be blank!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Phone number can not be blank!")]
		[Phone(ErrorMessage = "Invalid phone number!")]
		[StringLength(12)]
		public string? PhoneNumber { get; set; }

		public string? Gender { get; set; }

		[Required(ErrorMessage = "Date of birth can not be blank!")]
		public DateTime DateOfBirth { get; set; }
		public virtual Role? Role { get; set; }
		public virtual Experience? Experience { get; set; }
	}

	public static class UserExtension
	{
		/// <summary>
		/// A method to Convert User To UserResponse
		/// </summary>
		/// <param name="user">User to convert</param>
		/// <returns>UserResponse object base on the user</returns>
		public static UserResponse ToUserResponse(this User user)
		{
			return new UserResponse()
			{
				UserId = user.UserId,
				UserName = user.UserName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				DateOfBirth = user.DateOfBirth,
				Role = user.Role,
				Experience = user.Experience,
				Gender = user.Gender
			};
		}
	}
}