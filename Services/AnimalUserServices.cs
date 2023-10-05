using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalUserDTO;
using Services.Helper;

namespace Services
{
	public class AnimalUserServices : IAnimalUserServices
	{
		// prvate fields
		private readonly IAnimalUserRepositories _animalUserRepositories;
		private readonly IAnimalRepositories _animalRepositories;
		private readonly IUserRepositories _userRepositories;

		// constructor
		public AnimalUserServices(IAnimalUserRepositories animalUserRepositories)
		{
			_animalUserRepositories = animalUserRepositories;
		}
		public async Task<AnimalUserResponse> AddAnimalUser(AnimalUserAddRequest animalUserAddRequest)
		{
			ArgumentNullException.ThrowIfNull(animalUserAddRequest);

			// Check if zoo trainer is exist in database
			var zooTrainer = await _userRepositories.GetUserById(animalUserAddRequest.UserId);
			if (zooTrainer == null)
			{
				throw new ArgumentException("This Zoo Trainer is not exist!");
			}

			// Check if animal is exist in database
			var animal = await _animalRepositories.GetAnimalById(animalUserAddRequest.AnimalId);
			if (animal == null)
			{
				throw new ArgumentException("This Animal is not exist!");
			}

			// Check if zoo trainer is training this animal
			var relationshipExist = await _animalUserRepositories.GetAnimalUserRelationship
									(animalUserAddRequest.AnimalId
									, animalUserAddRequest.UserId);

			if(relationshipExist != null)
			{
				throw new ArgumentException("This Animal is being trained this Zoo Trainer!");
			}

			ValidationHelper.ModelValidation(animalUserAddRequest);

			AnimalUser animalUser = animalUserAddRequest.MapToAnimalUser();

			await _animalUserRepositories.Add(animalUser);

			return animalUser.ToAnimalUserResponse();

		}
	}
}
