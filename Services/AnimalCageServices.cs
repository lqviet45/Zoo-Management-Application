using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalCageDTO;
using Services.Helper;

namespace Services
{
	public class AnimalCageServices : IAnimalCageServices
	{
		// privvate filed
		private readonly IAnimalCageRepositories _animalCageRepositories;

		// constructor
		public AnimalCageServices(IAnimalCageRepositories animalCageRepositories)
		{
			_animalCageRepositories = animalCageRepositories;
		}

		public async Task<AnimalCageResponse> Add(AnimalCageAddRequest animalCageAddRequest)
		{
			ArgumentNullException.ThrowIfNull(animalCageAddRequest);

			var animalCageExist = await _animalCageRepositories.CheckAnimalCage(animalCageAddRequest.MapToAnimalCage());

			if(animalCageExist)
			{
				throw new ArgumentException("The animal is already in this cage at this specified day");
			}

			ValidationHelper.ModelValidation(animalCageAddRequest);

			AnimalCage animalCage = animalCageAddRequest.MapToAnimalCage();
			await _animalCageRepositories.Add(animalCage);

			return animalCage.ToAnimalCageResponse();
		}

		public async Task<List<AnimalCageResponse>> GetAllAnimalCage()
		{
			var listAnimalCage = await _animalCageRepositories.GetAllAnimalCage();

			var listAnimalCageResponse = listAnimalCage.Select(animalCage => animalCage.ToAnimalCageResponse())
										 .ToList();
			return listAnimalCageResponse;
		}

		public async Task<List<AnimalCageResponse>> GetAllAnimalInTheCage(int cageId)
		{
			var listAnimalCage = await _animalCageRepositories.GetAllAnimalInTheCage(cageId);
			var listAnimalCageResponse = listAnimalCage.Select(animalCage => animalCage.ToAnimalCageResponse()).ToList();
			return listAnimalCageResponse;
		}

		public async Task<List<AnimalCageResponse>> GetAnimalCageHistory(long animalId)
		{
			var listAnimalCage = await _animalCageRepositories.GetAnimalCageHistory(animalId);

			var listAnimalCageResponse = listAnimalCage.Select(animalCage => animalCage.ToAnimalCageResponse()).ToList();

			return listAnimalCageResponse;
		}

		public async Task<AnimalCageResponse> GetAnimalPresentCage(long animalId)
		{
			var animalCage = await _animalCageRepositories.GetAnimalPresentCage(animalId);

			if (animalCage is null)
			{
				throw new ArgumentException("The animal is not in any cage");
			}

			return animalCage.ToAnimalCageResponse();
		}
	}
	
}
