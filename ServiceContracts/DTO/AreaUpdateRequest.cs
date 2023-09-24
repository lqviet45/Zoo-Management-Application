using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class AreaUpdateRequest
	{
		[Required(ErrorMessage = "Area Name Can not be blank!")]
		public string? AreaName { get; set; }
	}
}
