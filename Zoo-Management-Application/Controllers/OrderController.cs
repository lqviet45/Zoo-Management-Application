using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.CustommerDTO;
using ServiceContracts.DTO.OrderDTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly ICustommerSevices _custommerSevices;
		private readonly IOrderSevices _orderSevices;
		private readonly ITicketServices _ticketServices;

		public OrderController(ICustommerSevices custommerSevices, IOrderSevices orderSevices, ITicketServices ticketServices)
		{
			_custommerSevices = custommerSevices;
			_orderSevices = orderSevices;
			_ticketServices = ticketServices;
		}

		[HttpPost]
		public async Task<IActionResult> PostOrder(CustommerAddRequest custommerAddRequest)
		{
			var custommer = await _custommerSevices.Add(custommerAddRequest);
			OrderAddRequest orderAddRequest = new()
			{
				CustommerId = custommer.CustommerId,
				PurchaseDate = DateTime.Now
			};
			var order = await _orderSevices.AddOrder(orderAddRequest);

			List<OrderDetailAddRequest> orderDetailAddRequests = new();
			foreach (var ticket in custommerAddRequest.Tickets)
			{
				orderDetailAddRequests.Add(new OrderDetailAddRequest()
				{
					OrderId = order.OrderId,
					TicketId = ticket.Key,
					Quantity = ticket.Value
				});
			}
			var orderDetailReponseList = await _orderSevices.AddOrderDetail(orderDetailAddRequests);

			order.OrderDetailResponses = orderDetailReponseList;

			return Ok(order);
		}

		[HttpGet("{orderId}")]
		public async Task<ActionResult<OrderResponse>> GetOrderById(long orderId)
		{
			var order = await _orderSevices.GetOrderById(orderId);
			if (order == null) return NotFound($"The order Id: {orderId} doesn't exist!");

			return Ok(order);
		}
	}
}
