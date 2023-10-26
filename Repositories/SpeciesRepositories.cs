using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
	public class SpeciesRepositories : ISpeciesRepositories
	{
		//private field
		private readonly ApplicationDbContext _dbContext;

		//constructor
		public SpeciesRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Species> Add(Species species)
		{
			
			_dbContext.Species.Add(species);
			await _dbContext.SaveChangesAsync();
			return species;
		}

		public async Task<bool> Delete(int speciesId)
		{
			var deleteSpecies = await _dbContext.Species
								.Where(species => species.SpeciesId == speciesId)
								.FirstOrDefaultAsync();

			if (deleteSpecies == null) 
			{
				return false; 
			}

			deleteSpecies.IsDeleted = true;

			 await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<List<Species>> GetAllSpecies()
		{
			var listSpecies = await _dbContext.Species
								.Where(temp => temp.IsDeleted == false)
								.ToListAsync();

			return listSpecies;
		}

		public async Task<List<Species>> GetFilteredSpecies(Expression<Func<Species, bool>> predicate)
		{
			return await _dbContext.Species.Where(predicate)
								   .ToListAsync();
		}

		public async Task<Species?> GetSpeciesById(int speciesId)
		{
			var species = await _dbContext.Species
							.Where(species => species.SpeciesId == speciesId 
							&& species.IsDeleted == false)
							.FirstOrDefaultAsync();

			return species;
		}

		public async Task<Species?> GetSpeciesByName(string speciesName)
		{
			return await _dbContext.Species
					.Where(species => species.SpeciesName == speciesName
					&& species.IsDeleted == false)
					.FirstOrDefaultAsync();
		}

		public async Task<Species> Update(Species species)
		{
			Species? matchingSpecies = await _dbContext.Species
									 .FirstOrDefaultAsync(species => species.SpeciesId == species.SpeciesId);

			if (matchingSpecies == null) { return species; }

			matchingSpecies.SpeciesName = species.SpeciesName;
			matchingSpecies.Family = species.Family;
			matchingSpecies.Information = species.Information;
			matchingSpecies.Characteristic = species.Characteristic;
			matchingSpecies.Ecological = species.Ecological;
			matchingSpecies.Allocation = species.Allocation;
			matchingSpecies.Diet = species.Diet;
			matchingSpecies.BreedingAndReproduction = species.BreedingAndReproduction;
			matchingSpecies.IsDeleted = species.IsDeleted;
			matchingSpecies.Image = species.Image;

			await _dbContext.SaveChangesAsync();

			return matchingSpecies;
		}
	}
}
