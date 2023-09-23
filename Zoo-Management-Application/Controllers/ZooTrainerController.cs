﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Zoo_Management_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ZooTrainerController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public ZooTrainerController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpGet]
		public async Task<ActionResult<UserResponse>> GetAllZooTrainer()
		{
			var listZooTrainer = await _userServices.GetAllZooTrainer();
			return Ok(listZooTrainer);
		}

		[HttpGet("{zooTrainerId}")]
		public async Task<IActionResult> GetZooTrainer(long zooTrainerId)
		{
			var mathcingZooTrainer = await _userServices.GetStaffById(zooTrainerId);
			if (mathcingZooTrainer == null)
			{
				return NotFound("The ZooTrainer Id dosen't exist!");
			}
			return Ok(mathcingZooTrainer);
		}
	}
}