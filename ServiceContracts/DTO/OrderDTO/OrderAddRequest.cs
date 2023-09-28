using Entities.Models;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO.OrderDTO
{
	public class OrderAddRequest
	{
		[Required(ErrorMessage = "Purchase date can not be empty!")]
		public DateTime PurchaseDate { get; set; }

		[Required]
		public long CustommerId { get; set; }

		public Order MaptoOrder() 
		{
			return new Order()
			{
				PurchaseDate = PurchaseDate,
				CustommerId = CustommerId
			};
		}
	}
}
