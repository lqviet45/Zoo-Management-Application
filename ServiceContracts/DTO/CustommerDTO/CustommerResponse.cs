using Entities.Models;
using ServiceContracts.DTO.OrderDTO;

namespace ServiceContracts.DTO.CustommerDTO
{
	public class CustommerResponse
	{
		public long CustommerId { get; set; }

		public string? Name { get; set; }

		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }

		public OrderResponse? OrderResponse { get; set; }
	}

	public static class CustommerExtension
	{
		public static CustommerResponse ToCustommerResponse(this Custommer custommer)
		{
			return new CustommerResponse()
			{
				CustommerId = custommer.CustommerId,
				Name = custommer.Name,
				Email = custommer.Email,
				PhoneNumber = custommer.PhoneNumber
			};
		}
	}
}
