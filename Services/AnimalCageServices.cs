using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalCageDTO;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.CageDTO;
using Services.Helper;

namespace Services
{
	public class AnimalCageServices : IAnimalCageServices
	{
		// privvate filed
		private readonly IAnimalCageRepositories _animalCageRepositories;
		private readonly IAnimalRepositories _animalRepositories;
		private readonly ICageRepositories _cageRepositories;

		// constructor
		public AnimalCageServices(IAnimalCageRepositories animalCageRepositories, IAnimalRepositories animalRepositories, ICageRepositories cageRepositories)
		{
			_animalCageRepositories = animalCageRepositories;
			_animalRepositories = animalRepositories;
			_cageRepositories = cageRepositories;
		}

		public async Task<AnimalCageResponse> UpdateAnimalCage(AnimalCageUpdateRequest animalCageUpdateRequest)
		{
			ArgumentNullException.ThrowIfNull(animalCageUpdateRequest);

			var animalCageExist = await _animalCageRepositories.CheckAnimalCage(animalCageUpdateRequest.MapToAnimalCage());

			if(animalCageExist)
			{
				throw new ArgumentException("The animal is already in this cage at this specified day");
			}

			var presentCage = await _animalCageRepositories.GetAnimalPresentCage(animalCageUpdateRequest.AnimalId);

			if(presentCage != null)
			{
				if(presentCage.CageId == animalCageUpdateRequest.CageId)
				{
					throw new ArgumentException("The animal is already in this cage");
				}
				else
				{
					var isMove = await _animalCageRepositories.MoveAnimalOut(presentCage.AnimalId);
				}
			}

			ValidationHelper.ModelValidation(animalCageUpdateRequest);

			AnimalCage animalCage = animalCageUpdateRequest.MapToAnimalCage();
			await _animalCageRepositories.Add(animalCage);

			return animalCage.ToAnimalCageResponse();
		}

		// This function is not used, but it is still here for future use
		public async Task<List<AnimalCageResponse>> GetAllAnimalCage()
		{
			var listAnimalCage = await _animalCageRepositories.GetAllAnimalCage();

			var listAnimalCageResponse = listAnimalCage.Select(animalCage => animalCage.ToAnimalCageResponse())
										 .ToList();
			return listAnimalCageResponse;
		}

		public async Task<List<AnimalResponse>> GetAllAnimalInTheCage(int cageId)
		{
			var listAnimal = await _animalCageRepositories.GetAllAnimalInTheCage(cageId);

			var listAnimalResponse = listAnimal.Select(animalCage => animalCage.ToAnimalCageResponse()).ToList();

			List<AnimalResponse> animalList = new List<AnimalResponse>();

			listAnimalResponse.ForEach(animal =>
			{
				var animalDetail = _animalRepositories.GetAnimalById(animal.AnimalId).Result;

				if (animalDetail != null)
				{
					animalList.Add(animalDetail.ToAnimalResponse());
				}

			});

			return animalList;

		}

		public async Task<List<CageResponse>> GetAnimalCageHistory(long animalId)
		{
			var listCage = await _animalCageRepositories.GetAnimalCageHistory(animalId);

			var listCageResponse = listCage.Select(animalCage => animalCage.ToAnimalCageResponse()).ToList();

			List<CageResponse> cageList = new List<CageResponse>();

			listCageResponse.ForEach(cage =>
			{
				var cageDetail = _cageRepositories.GetCageById(cage.CageId).Result;

				if (cageDetail != null)
				{
					cageList.Add(cageDetail.ToCageResponse());
				}

			});

			return cageList;
		}

		public async Task<CageResponse> GetAnimalPresentCage(long animalId)
		{
			var animalCage = await _animalCageRepositories.GetAnimalPresentCage(animalId);

			if (animalCage is null)
			{
				throw new ArgumentException("The animal is not in any cage");
			}

			var cage = await _cageRepositories.GetCageById(animalCage.CageId);

			if(cage is null)
			{
				throw new ArgumentException("The cage is not exist");
			}

			return cage.ToCageResponse();
		}

		public async Task<bool> MoveAnimalOut(long animalId)
		{
			var animalCage = await _animalCageRepositories.GetAnimalPresentCage(animalId);

			if (animalCage is null)
			{
				return false;
			}

			await _animalCageRepositories.MoveAnimalOut(animalId);

			return true;

		}

		public async Task<AnimalCageResponse> Add(AnimalCageAddRequest animalCageAddRequest)
		{
			ArgumentNullException.ThrowIfNull(animalCageAddRequest);

			var animalCageExist = await _animalCageRepositories.CheckAnimalCage(animalCageAddRequest.MapToAnimalCage());

			if (animalCageExist)
			{
				throw new ArgumentException("The animal is already in this cage at this specified day");
			}

			ValidationHelper.ModelValidation(animalCageAddRequest);

			AnimalCage animalCage = animalCageAddRequest.MapToAnimalCage();
			await _animalCageRepositories.Add(animalCage);

			return animalCage.ToAnimalCageResponse();
		}


	}
	
}
