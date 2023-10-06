using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

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
			var deleteSpecies = await _dbContext.Species.Where(species => species.SpeciesId == speciesId)
														.FirstOrDefaultAsync();

			if (deleteSpecies == null) { return false; }

			_dbContext.Species.Remove(deleteSpecies);

			int rowsDeleted = await _dbContext.SaveChangesAsync();

			return rowsDeleted > 0;
		}

		public async Task<List<Species>> GetAllSpecies()
		{
			var listSpecies = await _dbContext.Species.ToListAsync();

			return listSpecies;
		}

		public async Task<Species?> GetSpeciesById(int speciesId)
		{
			var species = await _dbContext.Species.Where(species => species.SpeciesId == speciesId)
											.FirstOrDefaultAsync();

			return species;
		}

		public async Task<Species?> GetSpeciesByName(string speciesName)
		{
			return await _dbContext.Species.Where(species => species.SpeciesName == speciesName)
									 .FirstOrDefaultAsync();
		}

		public async Task<Species> Update(Species species)
		{
			Species? matchingSpecies = await _dbContext.Species
									 .FirstOrDefaultAsync(species => species.SpeciesId == species.SpeciesId);

			if (matchingSpecies == null) { return species; }

			matchingSpecies.SpeciesName = species.SpeciesName;
			matchingSpecies.Description = species.Description;
			await _dbContext.SaveChangesAsync();

			return matchingSpecies;
		}
	}
}
