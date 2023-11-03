using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.CustommerDTO;
using ServiceContracts.DTO.EmailDTO;
using ServiceContracts.DTO.OrderDTO;
using ServiceContracts.DTO.TransReportDTO;
using Zoo.Management.Application.Filters.ActionFilters;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly ICustommerSevices _custommerSevices;
		private readonly IOrderSevices _orderSevices;
		private readonly IEmailServices _emailServices;

		public OrderController(ICustommerSevices custommerSevices, IOrderSevices orderSevices, IEmailServices emailServices)
		{
			_custommerSevices = custommerSevices;
			_orderSevices = orderSevices;
			_emailServices = emailServices;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
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

			double total = 0;

			foreach (var orderResponse in orderDetailReponseList)
			{
				if (orderResponse.Ticket != null)
					total += orderResponse.TotalPrice;
			}

			order.TotalPrice = total;

			await _orderSevices.UpdateOrderTotal(order.OrderId, total);

			_ = SendMail(order);

			return Ok(order);
		}

		[HttpGet("{OrderId}")]
		[TypeFilter(typeof(ValidateEntityExistsAttribute<Order>), Arguments = new object[] { "OrderId", typeof(long) })]
		public async Task<ActionResult<OrderResponse>> GetOrderById(long OrderId)
		{
			var order = await _orderSevices.GetOrderById(OrderId);
			if (order == null) return NotFound($"The order Id: {OrderId} doesn't exist!");

			double total = 0;
			foreach (var orderResponse in order.OrderDetailResponses)
			{
				if (orderResponse.Ticket != null)
					total += orderResponse.Ticket.Price * orderResponse.Quantity;
			}
			order.TotalPrice = total;
			return Ok(order);
		}

		[HttpGet("TransReport")]
		[Authorize(Roles = "Admin,OfficeStaff")]
		public async Task<IActionResult> GetTotalByDate(DateTime from, DateTime to, int ticketId = -1)
		{

			if (ticketId is -1)
			{
				var totalAll = await _orderSevices.GetTotalByDay(from, to);
				var listOrderDetails = await _orderSevices.GetOrderDetailsByDate(from, to);
				return Ok(new { totalAll, listOrderDetails });
			}

			var total = await _orderSevices.GetTotalByDay(from, to, ticketId);
			var listOrderDetail = await _orderSevices.GetOrderDetailByDate(from, to, ticketId);
			
			return Ok(new { total, listOrderDetail });
		}

		[HttpGet("transhistory")]
		[AllowAnonymous]
		public async Task<IActionResult> GetOrderByDate(DateTime from, DateTime to, int ticketId = -1)
		{

			if (ticketId is -1)
			{
				var totalAll = await _orderSevices.GetTotalByDay(from, to);
				var listOrderDetails = await _orderSevices.GetOrderByDate(from, to);
				return Ok(new { totalAll, listOrderDetails });
			}

			var total = await _orderSevices.GetTotalByDay(from, to, ticketId);
			var listOrder = await _orderSevices.GetOrderByDate(from, to, ticketId);

			return Ok(new { total, listOrder });
		}

		[HttpGet("revenue")]
		public async Task<IActionResult> GetRevenue(DateTime from, DateTime to, int ticketId = -1)
		{
			int totalQuantity;
			double totalRevenue;
			if (ticketId == -1)
			{
				totalRevenue = await _orderSevices.GetTotalByDay(from, to);
				totalQuantity = await _orderSevices.GetRevenue(to, from, ticketId);
				return Ok(new { totalQuantity, totalRevenue });
			}

			totalRevenue = await _orderSevices.GetTotalByDay(to, from, ticketId);
			totalQuantity = await _orderSevices.GetRevenue(from, to, ticketId);
			return Ok(new { totalQuantity, totalRevenue });
		}

		#region Send Mail
		private async Task SendMail(OrderResponse order)
		{
			string orderBody = string.Empty;
			foreach (var orderDetail in order.OrderDetailResponses)
			{
				if (orderDetail.Ticket != null)
				{
					orderBody += "<tr>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Ticket.TicketName}$</td>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Ticket.Price}$</td>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Quantity}$</td>\r\n" +
						$"				<td style=\"padding-right: 50px;\">{orderDetail.TotalPrice}$</td>\r\n" +
						"        </tr>";
				}
			}
			EmailDto email = new EmailDto();
			if (order.Custommer != null)
			{
				email.To = order.Custommer.Email;
				email.Subject = "SaiGonZoo";

				string emailBodySend = emailBody.Replace("OrderId", order.OrderId.ToString())
					.Replace("custommerName", order.Custommer.Name)
					.Replace("custommerPhone", order.Custommer.PhoneNumber)
					.Replace("custommerEmail", order.Custommer.Email)
					.Replace("orderBody", orderBody)
					.Replace("allTotal", order.TotalPrice.ToString());

				email.Body = emailBodySend;
			}

			await _emailServices.SendEmail(email);
		}

		private readonly string emailBody = $"<div>\r\n" +
			"        <p>Thank you for purchasing our ticket</p>\r\n" +
			"        <p style=\"color: #02ACEA;\">Here is Order's Information Order ID: OrderId</p>\r\n" +
			"		 <p>You can use Order Id to go to the website to search for your order information there</p>" +
			"        <div>\r\n" +
			"            <p>Customer information</p>\r\n" +
			"            <p>Full name: custommerName</p>\r\n" +
			"            <p>Phone number: custommerPhone</p>\r\n" +
			"            <p>Email: custommerEmail</p>\r\n" +
			"        </div>\r\n\r\n" +
			"        <div>\r\n" +
			"            <p style=\"color: #02ACEA;\">Order's detail</p>\r\n" +
			"            <div>\r\n" +
			"                <table>\r\n" +
			"                    <thead>\r\n" +
			"                        <tr>\r\n" +
			"                            <th style=\"text-align: start;\">Product's name</th>\r\n" +
			"                            <th style=\"text-align: start;\">Price</th>\r\n" +
			"                            <th style=\"text-align: start;\">Quantity</th>\r\n" +
			"                        </tr>\r\n" +
			"                    </thead>\r\n" +
			"                    <tbody>\r\n" +
			"							orderBody" +
			"                    </tbody>\r\n" +
			"                    <tfoot>\r\n" +
			"                        <tr>\r\n" +
			"                            <td colspan=\"2\"></td>\r\n" +
			"                            <td style=\"padding-right: 20px;\">Total order value</td>\r\n" +
			"                            <td>allTotal$</td>\r\n" +
			"                        </tr>\r\n" +
			"                    </tfoot>\r\n" +
			"                </table>\r\n" +
			"            </div>\r\n" +
			"        </div>\r\n" +
			"    </div>";

		#endregion
	}
}
