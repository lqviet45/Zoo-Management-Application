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
}
