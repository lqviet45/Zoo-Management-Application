using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using ServiceContracts;
using ServiceContracts.DTO.EmailDTO;
using ServiceContracts.DTO.MealDTO;
using System.Threading.Tasks;

namespace Infrastructure
{
	[DisallowConcurrentExecution]
	public class FeedingTimeNotificationBackgroundJob : IJob
	{
		private readonly IConfiguration _configuration;
		private readonly IEmailServices _emailServices;
		private readonly IMealServices _mealServices;
		private readonly IUserServices _userServices;
		
		

		public FeedingTimeNotificationBackgroundJob(IConfiguration configuration, IEmailServices emailServices, IMealServices mealServices, IUserServices userServices)
		{
			
			_configuration = configuration;
			_emailServices = emailServices;
			_mealServices = mealServices;
			_userServices = userServices;
		}

		public async Task Execute(IJobExecutionContext context)
		{

			// Get the current time with seconds and milliseconds set to zero
			DateTime currentTime = DateTime.Now;
			DateTime currentTimeWithoutSeconds = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, 0);

			// Calculate the time 30 minutes from now with seconds and milliseconds set to zero
			DateTime notificationTime = currentTimeWithoutSeconds.AddMinutes(30);

			// Get all meals
			List<MealResponse> allMeals = await _mealServices.GetAllMeal();

			// Filter meals with feeding times 30 minutes after the current time
			List<MealResponse> mealsToNotify = allMeals
				.Where(meal => meal.FeedingTime == notificationTime.TimeOfDay)
				.ToList();

			// Extract unique user and animal IDs from the filtered meals
			var uniqueUserIds = mealsToNotify.Select(meal => meal.animalUser.UserId).Distinct();
			var uniqueAnimalIds = mealsToNotify.Select(meal => meal.animalUser.AnimalId).Distinct();

			// Send email notifications to the unique users or take any other required actions
			foreach (var userId in uniqueUserIds)
			{
				// Get the user based on userId if needed
				var user = await _userServices.GetZooTrainerById(userId);

				if (user != null)
				{
					// Send email notifications or take any other action for this user
					// You can use the _emailServices to send emails
					var emailDto = new EmailDto
					{
						To = user.Email,
						Subject = "Feeding Time Notification",
						Body = $"Hello {user.FullName}, your assigned animal(s) will be fed at {notificationTime}."
					};

					await _emailServices.SendEmail(emailDto);
				}

			}

			await Task.CompletedTask;
		}

	}
}
