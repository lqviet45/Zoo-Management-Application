using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AuthenDTO
{
	public class LoginUserDTO
	{
		[Required(ErrorMessage = "UserName can not be empty!")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password can not be empty!")]
		public string Password { get; set; } = string.Empty;
	}
}
