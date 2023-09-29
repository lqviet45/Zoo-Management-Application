using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.OrderDTO;

namespace Services
{
	public class OrderSevices : IOrderSevices
	{
		private readonly IOrderReponsitories _orderReponsitories;

		public OrderSevices(IOrderReponsitories orderReponsitories)
		{
			_orderReponsitories = orderReponsitories;
		}

		public Task<OrderResponse> AddOrder(OrderAddRequest? orderAddRequest)
		{
			throw new NotImplementedException();
		}

		public Task<OrderResponse?> GetOrderById(long orderId)
		{
			throw new NotImplementedException();
		}
	}
}
