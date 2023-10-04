using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.CustommerDTO;
using ServiceContracts.DTO.EmailDTO;
using ServiceContracts.DTO.OrderDTO;

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

			await SendMail(order);

			return Ok(order);
		}

		[HttpGet("{orderId}")]
		public async Task<ActionResult<OrderResponse>> GetOrderById(long orderId)
		{
			var order = await _orderSevices.GetOrderById(orderId);
			if (order == null) return NotFound($"The order Id: {orderId} doesn't exist!");

			double total = 0;
			foreach (var orderResponse in order.OrderDetailResponses)
			{
				if (orderResponse.Ticket != null)
					total += orderResponse.Ticket.Price * orderResponse.Quantity;
			}
			order.TotalPrice = total;
			return Ok(order);
		}

		private async Task SendMail(OrderResponse order)
		{
			string orderBody = string.Empty;
			foreach (var orderDetail in order.OrderDetailResponses)
			{
				if (orderDetail.Ticket != null)
				{
					orderBody += "<tr>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Ticket.TicketName}</td>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Ticket.Price}</td>\r\n" +
						$"              <td style=\"padding-right: 50px;\">{orderDetail.Quantity}</td>\r\n" +
						"        </tr>";
				}
			}
			EmailDto email = new EmailDto();
			if (order.Custommer != null)
			{
				email.To = order.Custommer.Email;
				email.Subject = "Thảo cầm viên";

				string emailBodySend = emailBody.Replace("orderId", order.OrderId.ToString())
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
			"        <p>Cảm ơn quý khách đã mau vé</p>\r\n" +
			"        <p style=\"color: #02ACEA;\">Thông tin đơn hàng Order ID: orderId</p>\r\n" +
			"		 <p>Bạn có thể dùng order Id để lên web tìm kiếm thông tin đơn hàng của bạn ở trên đó</p>" +
			"        <div>\r\n" +
			"            <p>Thông tin khách hàng</p>\r\n" +
			"            <p>Họ tên: custommerName</p>\r\n" +
			"            <p>Số điện thoại: custommerPhone</p>\r\n" +
			"            <p>Email: custommerEmail</p>\r\n" +
			"        </div>\r\n\r\n" +
			"        <div>\r\n" +
			"            <p style=\"color: #02ACEA;\">Chi tiết đơn hàng</p>\r\n" +
			"            <div>\r\n" +
			"                <table>\r\n" +
			"                    <thead>\r\n" +
			"                        <tr>\r\n" +
			"                            <th style=\"text-align: start;\">Tên sản phẩm</th>\r\n" +
			"                            <th style=\"text-align: start;\">Giá</th>\r\n" +
			"                            <th style=\"text-align: start;\">Số lượng</th>\r\n" +
			"                        </tr>\r\n" +
			"                    </thead>\r\n" +
			"                    <tbody>\r\n" +
			"							orderBody" +
			"                    </tbody>\r\n" +
			"                    <tfoot>\r\n" +
			"                        <tr>\r\n" +
			"                            <td colspan=\"1\"></td>\r\n" +
			"                            <td style=\"padding-right: 20px;\">Tổng giá trị đơn hàng</td>\r\n" +
			"                            <td>allTotal</td>\r\n" +
			"                        </tr>\r\n" +
			"                    </tfoot>\r\n" +
			"                </table>\r\n" +
			"            </div>\r\n" +
			"        </div>\r\n" +
			"    </div>";
	}
}
