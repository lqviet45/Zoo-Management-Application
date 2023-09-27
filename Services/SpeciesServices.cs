using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.SpeciesDTO;
using Services.Helper;

namespace Services
{
    public class SpeciesServices : ISpeciesServices
	{
		// private field
		private readonly ISpeciesRepositories _speciesRepositories;

		// constructor
		public SpeciesServices(ISpeciesRepositories speciesRepositories)
		{
			_speciesRepositories = speciesRepositories;
		}
		public async Task<SpeciesResponse> AddSpecies(SpeciesAddRequest? speciesAddRequest)
		{
			ArgumentNullException.ThrowIfNull(speciesAddRequest);

			// Check Duplicate SpeciesName
			var speciesExist = await _speciesRepositories.GetSpeciesByName(speciesAddRequest.SpeciesName);
			if (speciesExist != null)
			{
				throw new ArgumentException("The SpeciesName is exist!");
			}

			ValidationHelper.ModelValidation(speciesAddRequest);

			Species species = speciesAddRequest.MapToSpecies();

			await _speciesRepositories.Add(species);

			return species.ToSpeciesResponse();
		}

		public async Task<bool> DeleteSpecies(int? id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));

			Species? species = await _speciesRepositories.GetSpeciesById(id.Value);

			if (species == null)
			{
				return false;
			}

			await _speciesRepositories.Delete(id.Value);

			return true;
		}

		public async Task<List<SpeciesResponse>> GetAllSpecies()
		{
			var listSpecies = await _speciesRepositories.GetAllSpecies();

			return listSpecies.Select(temp => temp.ToSpeciesResponse()).ToList();
		}

		public async Task<SpeciesResponse?> GetSpeciesById(int? id)
		{
			if(id == null) return null;

			var species = await _speciesRepositories.GetSpeciesById(id.Value);

			if (species == null)
			{
				return null;
			}

			return species.ToSpeciesResponse();
		}

		public async Task<SpeciesResponse> UpdateSpecies(SpeciesUpdateRequest? speciesUpdateRequest)
		{
			if(speciesUpdateRequest == null) throw new ArgumentNullException(nameof(speciesUpdateRequest));

			ValidationHelper.ModelValidation(speciesUpdateRequest);

			Species? matchingSpecies = await _speciesRepositories.GetSpeciesById(speciesUpdateRequest.SpeciesId);

			if (matchingSpecies == null)
			{
				throw new ArgumentException("The SpeciesId is not exist!");
			}

			matchingSpecies.SpeciesName = speciesUpdateRequest.SpeciesName;
			matchingSpecies.Description = speciesUpdateRequest.Description;

			await _speciesRepositories.Update(matchingSpecies);

			return matchingSpecies.ToSpeciesResponse();
		}
	}
}
