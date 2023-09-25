using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class AreaUpdateRequest
	{
		[Required(ErrorMessage = "AreaId can not be blank!")]
		public int AreaId { get; set; }

		[Required(ErrorMessage = "Area Name Can not be blank!")]
		public string? AreaName { get; set; }

		public bool IsDelete { get; set; }

		/// <summary>
		/// Converts the current obj of AreaUpdateRequest to Area obj
		/// </summary>
		/// <returns>Returns Area obj</returns>
		public Area MapToArea()
		{
			return new Area
			{
				AreaId = AreaId,
				AreaName = AreaName,
				IsDelete = IsDelete
			};
		}
	}
}
