using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.CustommerDTO;

namespace Services
{
	public class CustommerSevices : ICustommerSevices
	{
		private readonly ICustommerReponsitories _custommerReponsitories;

		public CustommerSevices(ICustommerReponsitories custommerReponsitories)
		{
			_custommerReponsitories = custommerReponsitories;
		}

		public async Task<CustommerResponse> Add(CustommerAddRequest? custommerAddRequest)
		{
			if (custommerAddRequest is null)
				throw new ArgumentNullException("The custommer to adding is empty!");

			var custommer = custommerAddRequest.MapToCustommer();

			await _custommerReponsitories.AddCustommer(custommer);
						
			return custommer.ToCustommerResponse();
		}

		public async Task<List<CustommerResponse>> GetCustommerByEmail(string email)
		{
			var listCustommer = await _custommerReponsitories.GetCustommerByEmail(email);

			return listCustommer.Select(c => c.ToCustommerResponse()).ToList();
		}

		public async Task<CustommerResponse> GetCustommerById(long custommerId)
		{
			var custommer = await _custommerReponsitories.GetCustommerById(custommerId);
			if (custommer is null)
			{
				throw new ArgumentNullException($"The custommer Id: {custommerId} doesn't exist!");
			}

			return custommer.ToCustommerResponse();
		}
	}
}
