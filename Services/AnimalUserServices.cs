using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AnimalDTO;
using ServiceContracts.DTO.AnimalUserDTO;
using ServiceContracts.DTO.UserDTO;
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

			// Check if user is exist in database
			var animal = await _animalRepositories.GetAnimalById(animalUserAddRequest.AnimalId);
			if (animal == null)
			{
				throw new ArgumentException("This Animal is not exist!");
			}

			// Check if zoo trainer is training this user
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

		public async Task<List<AnimalResponse>> GetAnimalByZooTrainerId(long? userId)
		{
			var listAnimal = await _animalUserRepositories.GetAnimalByZooTrainerId(userId);

			var listAnimalResponse = listAnimal.Select(animal => animal.ToAnimalUserResponse()).ToList();

			List<AnimalResponse> animaList = new List<AnimalResponse>();

			listAnimalResponse.ForEach(animal =>
			{
				var animalDetail = _animalRepositories.GetAnimalById(animal.AnimalId).Result;

				if (animalDetail != null)
				{
					animaList.Add(animalDetail.ToAnimalResponse());
				}
			});

			return animaList;
		}

		public async Task<List<UserResponse>> GetZooTrainerByAnimalId(long? animalId)
		{
			var listUser = await _animalUserRepositories.GetAnimalByZooTrainerId(animalId);

			var listUserResponse = listUser.Select(user => user.ToAnimalUserResponse()).ToList();

			List<UserResponse> userList = new List<UserResponse>();

			listUserResponse.ForEach(user =>
			{
				var userDetail = _userRepositories.GetZooTrainerById(user.UserId).Result;

				if (userDetail != null)
				{
					userList.Add(userDetail.ToUserResponse());
				}
			});

			return userList;
		}
	
	}
}
