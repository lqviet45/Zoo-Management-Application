using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly ICustommerSevices _custommerSevices;

		public OrderController(ICustommerSevices custommerSevices)
		{
			_custommerSevices = custommerSevices;
		}


	}
}
