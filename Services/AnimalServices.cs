using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;
using Services.Helper;

namespace Services
{
    public class AnimalServices : IAnimalServices
	{
		// private fields
		private readonly IAnimalRepositories _animalRepositories;
		private readonly IAnimalUserRepositories _animalUserRepositories;
		private readonly IUserRepositories _userRepositories;
		private readonly ICageRepositories _cageRepositories;
		private readonly IAnimalCageRepositories _animalCageRepositories;

		// constructor
		public AnimalServices(IAnimalRepositories animalRepositories, IAnimalUserRepositories animalUserRepositories, IUserRepositories userRepositories, ICageRepositories cageRepositories, IAnimalCageRepositories animalCageRepositories)
		{
			_animalRepositories = animalRepositories;
			_animalUserRepositories = animalUserRepositories;
			_userRepositories = userRepositories;
			_cageRepositories = cageRepositories;
			_animalCageRepositories = animalCageRepositories;
		}
		public async Task<AnimalResponse> AddAnimal(AnimalAddRequest animaladd)
		{
			ArgumentNullException.ThrowIfNull(animaladd);

			var ExistAnimal = await _animalRepositories.GetAnimalByName(animaladd.AnimalName);

			if (ExistAnimal != null)
			{
				throw new ArgumentException("The animal name is exist!");
			}

			ValidationHelper.ModelValidation(animaladd);

			Animal animal = animaladd.MapToAnimal();
			await _animalRepositories.Add(animal);

			var zootrainer = await _userRepositories.GetUserById(animaladd.userId);

			if (zootrainer == null)
			{
				throw new ArgumentException("The zoo trainer id doesn't exist!");
			}

			var cage = _cageRepositories.GetCageById(animaladd.cageId); 

			if(cage == null)
			{
				throw new ArgumentException("The cage id doesn't exist!");
			}

			await _animalUserRepositories.Add(new AnimalUser
			{
				AnimalId = animal.AnimalId,
				UserId = animaladd.userId
			});

			await _animalCageRepositories.Add(new AnimalCage
			{
				AnimalId = animal.AnimalId,
				CageId = animaladd.cageId,
				IsIn = true,
				DayIn = DateTime.Now
			});

			return animal.ToAnimalResponse();
		}

		public async Task<bool> DeleteAnimal(long animalId)
		{
			var deleteAnimal = await _animalRepositories.GetAnimalById(animalId);

			if(deleteAnimal is null)
			{
				return false;
			}

			await _animalRepositories.DeleteAnimalById(animalId);

			return true;

		}

		public async Task<AnimalResponse?> GetAnimalById(long animalId)
		{
			var matchingAnimal = await _animalRepositories.GetAnimalById(animalId);
			if (matchingAnimal == null)
			{
				return null;
			}

			return matchingAnimal.ToAnimalResponse();
		}

		public async Task<List<AnimalResponse>> GetAnimalList()
		{
			var animalList = await _animalRepositories.GetAllAnimal();

			var animalListResponse = animalList.Select(a => a.ToAnimalResponse()).ToList();

			return animalListResponse;
		}

		public async Task<List<AnimalResponse>> GetFiteredAnimal(string searchBy, string? searchString)
		{
			if(string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<Animal> animals = searchBy switch
			{
				nameof(AnimalResponse.AnimalName) => 
				await _animalRepositories.GetFilteredAnimal(temp => 
						temp.AnimalName.Contains(searchString) && temp.IsDelete == false),

				nameof(AnimalResponse.Species.SpeciesName) => 
				await _animalRepositories.GetFilteredAnimal(temp => 
						temp.Species.SpeciesName.Contains(searchString) && temp.IsDelete == false),

				nameof(AnimalResponse.Status) => 
				await _animalRepositories.GetFilteredAnimal(temp => 
						temp.Status.Contains(searchString) && temp.IsDelete == false),

				_ => await _animalRepositories.GetAllAnimal()
			};

			return animals.Select(animal => animal.ToAnimalResponse()).ToList();
		}

		public async Task<AnimalResponse> UpdateAnimal(AnimalUpdateRequest animalUpdateRequest)
		{
			if(animalUpdateRequest == null)
			{
				throw new ArgumentNullException(nameof(animalUpdateRequest));
			}
			
			ValidationHelper.ModelValidation(animalUpdateRequest);

			var updateAnimal = await _animalRepositories.GetAnimalById(animalUpdateRequest.AnimalId);

			if(updateAnimal == null)
			{
				throw new ArgumentException("Given animal doesn't exsit");
			}

			updateAnimal.AnimalName = animalUpdateRequest.AnimalName;
			updateAnimal.SpeciesId = animalUpdateRequest.SpeciesId;
			updateAnimal.DateArrive = animalUpdateRequest.DateArrive;
			updateAnimal.Status = animalUpdateRequest.Status;
			updateAnimal.IsDelete = animalUpdateRequest.IsDelete;

			await _animalRepositories.UpdateAnimal(updateAnimal);

			return updateAnimal.ToAnimalResponse();
		}



		

	}
}
