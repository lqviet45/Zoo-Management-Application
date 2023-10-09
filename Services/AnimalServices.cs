using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalAddDTO;
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

		// constructor
		public AnimalServices(IAnimalRepositories animalRepositories, IAnimalUserRepositories animalUserRepositories, IUserRepositories userRepositories)
		{
			_animalRepositories = animalRepositories;
			_animalUserRepositories = animalUserRepositories;
			_userRepositories = userRepositories;
		}
		public async Task<AnimalResponse> AddAnimal(AnimalAdd animaladd)
		{
			ArgumentNullException.ThrowIfNull(animaladd);

			var ExistAnimal = await _animalRepositories.GetAnimalByName(animaladd.AnimalAddRequest.AnimalName);

			if (ExistAnimal != null)
			{
				throw new ArgumentException("The animal name is exist!");
			}

			ValidationHelper.ModelValidation(animaladd);

			Animal animal = animaladd.AnimalAddRequest.MapToAnimal();
			await _animalRepositories.Add(animal);

			var zootrainer = await _userRepositories.GetUserById(animaladd.userId);

			if (zootrainer == null)
			{
				throw new ArgumentException("The zoo trainer id doesn't exist!");
			}

			await _animalUserRepositories.Add(new AnimalUser
			{
				AnimalId = animal.AnimalId,
				UserId = animaladd.userId
			});

			return animal.ToAnimalResponse();
		}

		public Task<bool> DeleteAnimal(long animalId)
		{
			throw new NotImplementedException();
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

		public Task<List<AnimalResponse>> GetAnimalList()
		{
			throw new NotImplementedException();
		}

		public Task<AnimalResponse> UpdateAnimal(AnimalUpdateRequest animalUpdateRequest)
		{
			throw new NotImplementedException();
		}



		

	}
}
