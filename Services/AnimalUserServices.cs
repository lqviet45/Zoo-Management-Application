using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;
using Services.Helper;
using System.Reflection.Metadata.Ecma335;

namespace Services
{
	public class AnimalUserServices : IAnimalUserServices
	{
		// prvate fields
		private readonly IAnimalUserRepositories _animalUserRepositories;
		private readonly IAnimalRepositories _animalRepositories;
		private readonly IUserRepositories _userRepositories;

		// constructor
		public AnimalUserServices(IAnimalUserRepositories animalUserRepositories, IAnimalRepositories animalRepositories, IUserRepositories userRepositories)
		{
			_animalUserRepositories = animalUserRepositories;
			_animalRepositories = animalRepositories;
			_userRepositories = userRepositories;
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

		public async Task<bool> DeleteAnimalUser(long animalId, long userId)
		{
			var animalUser = await _animalUserRepositories.GetAnimalUserRelationship(animalId, userId);

			if (animalUser == null)
			{
				return false;
			}

			await _animalUserRepositories.Delete(animalId, userId);

			return true;

		}

		public async Task<List<AnimalUserResponse>> GetAnimalByZooTrainerId(long? userId)
		{
			var listAnimal = await _animalUserRepositories.GetAnimalByZooTrainerId(userId);

			var listAnimalResponse = listAnimal.Select(animal => animal.ToAnimalUserResponse()).ToList();

			listAnimalResponse.ForEach(  async animal =>
			{
				var animalDetail = await _animalRepositories.GetAnimalById(animal.AnimalId);

				animal.AnimalResponse  = animalDetail.ToAnimalResponse();
			});

			return listAnimalResponse;
		}

		public async Task<List<AnimalUserResponse>> GetZooTrainerByAnimalId(long? animalId)
		{
			var listZooTrainer = await _animalUserRepositories.GetZooTrainerByAnimalId(animalId);

			var listZooTrainerResponse = listZooTrainer.Select(zooTrainer => zooTrainer.ToAnimalUserResponse()).ToList();

			listZooTrainerResponse.ForEach(async zooTrainer =>
			{
				var zooTrainerDetail = await _userRepositories.GetUserById(zooTrainer.UserId);

				zooTrainer.UserResponse = zooTrainerDetail.ToUserResponse();
			});

			return listZooTrainerResponse;
		}
	}
}
