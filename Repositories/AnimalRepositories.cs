using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


namespace Repositories
{
	public class AnimalRepositories : IAnimalRepositories
	{
		// private fields
		private readonly ApplicationDbContext _dbContext;
		private readonly ISpeciesRepositories _speciesRepositories;

		// constructor
		public AnimalRepositories(ApplicationDbContext dbContext, ISpeciesRepositories speciesRepositories)
		{
			_dbContext = dbContext;
			_speciesRepositories = speciesRepositories;
		}
		public async Task<Animal> Add(Animal animal)
		{
			var species = await _speciesRepositories.GetSpeciesById(animal.SpeciesId);

			if (species == null)
			{
				throw new ArgumentException("The species is not exist!");
			}

			animal.IsDelete = false;

			_dbContext.Animals.Add(animal);

			animal.Species = species;

			await _dbContext.SaveChangesAsync();


			return animal;
		}

		public async Task<bool> DeleteAnimalById(long animalId)
		{
			var animalDelete = await GetAnimalById(animalId);
			if (animalDelete is null)
			{
				return false;
			}
			animalDelete.IsDelete = true;
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public Task<List<Animal>> GetAllAnimal()
		{
			var listAnimal = _dbContext.Animals.Where(animal => animal.IsDelete == false)
				.Include(a => a.Species)
				.Include(a => a.AnimalLink)
				.Include(a => a.AnimalZooTrainers)
				.Include(a => a.AnimalCages)
				.ToListAsync();

			return listAnimal;
		}

		public async Task<Animal?> GetAnimalById(long animalId)
		{
			var matchingAnimal = await _dbContext.Animals
									.Where(animal => animal.AnimalId == animalId 
											&& animal.IsDelete == false)
									.Include(a => a.Species)
									.Include(a => a.AnimalLink)
									.Include(a => a.AnimalZooTrainers)
									.Include(a => a.AnimalCages)
									.FirstOrDefaultAsync();

			return matchingAnimal;
		}

		public Task<Animal?> GetAnimalByName(string? name)
		{
			var matchingAnimal = _dbContext.Animals
								.Where(animal => animal.AnimalName == name 
								&& animal.IsDelete == false)
								.FirstOrDefaultAsync();
			return matchingAnimal;
		}

		public Task<List<Animal>> GetAnimalBySpeciesId(long speciesId)
		{
			var matchingAnimal = _dbContext.Animals
									.Where(animal => animal.SpeciesId == speciesId 
											&& animal.IsDelete == false)
									.Include(a => a.Species)
									.Include(a => a.AnimalLink)
									.Include(a => a.AnimalZooTrainers)
									.Include(a => a.AnimalCages)
									.ToListAsync();
			return matchingAnimal;
		}

		public async Task<Animal> UpdateAnimal(Animal animal)
		{
			var updateAnimal = _dbContext.Animals
								.FirstOrDefault(a => a.AnimalId == animal.AnimalId 
								&& animal.IsDelete == false);

			if (updateAnimal is null)
			{
				return animal;
			}

			updateAnimal.AnimalName = animal.AnimalName;
			updateAnimal.DateArrive = animal.DateArrive;
			updateAnimal.Status = animal.Status;
			updateAnimal.SpeciesId = animal.SpeciesId;
			updateAnimal.IsDelete = animal.IsDelete;
			//updateAnimal.AnimalZooTrainers = animal.AnimalZooTrainers;
			//updateAnimal.AnimalLink = animal.AnimalLink;
			//updateAnimal.AnimalCages = animal.AnimalCages;

			await _dbContext.SaveChangesAsync();
			return updateAnimal;
		}
	}
}
